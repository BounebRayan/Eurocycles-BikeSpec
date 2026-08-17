Public Class FormListe

    Private Const SearchPlaceholder As String = "Rechercher : code, nom, marque…"

    Private ReadOnly _repository As New NomenclatureRepository()

    ' --- Single-window navigation state -----------------------------------------------------
    ' Nouveau/Modifier/Aperçu no longer open separate top-level windows: FormNomenclature and
    ' FormApercu are embedded (TopLevel=False, Dock=Fill) into pnlContent in place of the list
    ' view, so clicking around between Edit and Aperçu updates this one window's content instead
    ' of stacking new windows. When navigating from Edit to its Aperçu preview, the edit form is
    ' only detached (not closed), so its in-progress unsaved state survives the round trip.

    Private _activeContentForm As Form
    Private _suspendedEditForm As FormNomenclature
    Private _suspendedEditSubtitle As String

    Private Sub FormListe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Theme.ApplyPlaceholder(txtSearch, SearchPlaceholder)
        dgvNomenclatures.DataSource = bsNomenclatures
        LoadAll()
    End Sub

    Private Sub LoadAll()
        Try
            Dim list = _repository.GetAll()
            bsNomenclatures.DataSource = list
            UpdateStatus(list.Count)
        Catch ex As DataAccessException
            ShowDataError(ex)
        End Try
    End Sub

    Private Sub PerformSearch()
        Dim term = Theme.GetRealText(txtSearch).Trim()
        Try
            Dim list = If(term.Length = 0, _repository.GetAll(), _repository.Search(term))
            bsNomenclatures.DataSource = list
            UpdateStatus(list.Count)
        Catch ex As DataAccessException
            ShowDataError(ex)
        End Try
    End Sub

    Private Sub UpdateStatus(count As Integer)
        If count = 0 Then
            lblStatusCount.Text = "Aucune nomenclature trouvée."
        ElseIf count = 1 Then
            lblStatusCount.Text = "1 nomenclature trouvée."
        Else
            lblStatusCount.Text = $"{count} nomenclatures trouvées."
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        PerformSearch()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PerformSearch()
        End If
    End Sub

    Private Sub btnActualiser_Click(sender As Object, e As EventArgs) Handles btnActualiser.Click
        txtSearch.Clear()
        Theme.ShowPlaceholderIfEmpty(txtSearch, SearchPlaceholder)
        LoadAll()
    End Sub

    Private Sub dgvNomenclatures_SelectionChanged(sender As Object, e As EventArgs) Handles dgvNomenclatures.SelectionChanged
        Dim hasSelection = dgvNomenclatures.SelectedRows.Count > 0
        btnModifier.Enabled = hasSelection
        btnSupprimer.Enabled = hasSelection
        btnApercu.Enabled = hasSelection
    End Sub

    Private Function SelectedNomenclature() As Nomenclature
        If dgvNomenclatures.SelectedRows.Count = 0 Then Return Nothing
        Return TryCast(dgvNomenclatures.SelectedRows(0).DataBoundItem, Nomenclature)
    End Function

    Private Sub ShowDataError(ex As DataAccessException)
        MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub btnSupprimer_Click(sender As Object, e As EventArgs) Handles btnSupprimer.Click
        Dim selected = SelectedNomenclature()
        If selected Is Nothing Then Return

        Dim confirm = MessageBox.Show(
            $"Supprimer la nomenclature '{selected.Code}' ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If confirm <> DialogResult.Yes Then Return

        Try
            _repository.Delete(selected.Code)
            LoadAll()
        Catch ex As DataAccessException
            ShowDataError(ex)
        End Try
    End Sub

    ' --- Content-swap plumbing ---------------------------------------------------------------

    ''' <summary>Shows the list view (search/grid/footer), discarding any embedded content.</summary>
    Private Sub ShowListView()
        pnlContent.Controls.Clear() ' detaches only - never disposes (Close() on the child does that)
        _activeContentForm = Nothing
        _suspendedEditForm = Nothing
        pnlContent.Visible = False
        pnlListView.Visible = True
        lblHeaderSubtitle.Text = "BikeSpec » Liste des nomenclatures"
    End Sub

    ''' <summary>Embeds childForm into pnlContent in place of whatever is currently shown.</summary>
    Private Sub ShowEmbedded(childForm As Form, subtitle As String)
        pnlContent.Controls.Clear()

        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.Dock = DockStyle.Fill
        pnlContent.Controls.Add(childForm)
        childForm.Show()

        _activeContentForm = childForm
        pnlListView.Visible = False
        pnlContent.Visible = True
        lblHeaderSubtitle.Text = subtitle
    End Sub

    ' --- Edit (Nouveau / Modifier) -----------------------------------------------------------

    Private Sub btnNouveau_Click(sender As Object, e As EventArgs) Handles btnNouveau.Click
        OpenEditor(Nothing)
    End Sub

    Private Sub btnModifier_Click(sender As Object, e As EventArgs) Handles btnModifier.Click
        OpenEditForSelected()
    End Sub

    Private Sub dgvNomenclatures_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvNomenclatures.CellDoubleClick
        If e.RowIndex >= 0 Then
            btnApercu_Click(sender, e)
        End If
    End Sub

    Private Sub OpenEditForSelected()
        Dim selected = SelectedNomenclature()
        If selected Is Nothing Then Return
        OpenEditor(selected)
    End Sub

    ''' <summary>nomenclature = Nothing opens New mode; otherwise opens Edit mode for it.</summary>
    Private Sub OpenEditor(nomenclature As Nomenclature)
        Dim editForm As FormNomenclature
        Dim subtitle As String
        If nomenclature Is Nothing Then
            editForm = New FormNomenclature()
            subtitle = "BikeSpec » Nouvelle nomenclature"
        Else
            editForm = New FormNomenclature(nomenclature)
            subtitle = $"BikeSpec » Modifier · {nomenclature.Code}"
        End If

        AddHandler editForm.FormClosed, AddressOf EditForm_FormClosed
        AddHandler editForm.PreviewRequested, AddressOf EditForm_PreviewRequested
        ShowEmbedded(editForm, subtitle)
    End Sub

    Private Sub EditForm_FormClosed(sender As Object, e As FormClosedEventArgs)
        Dim editForm = DirectCast(sender, FormNomenclature)
        RemoveHandler editForm.FormClosed, AddressOf EditForm_FormClosed
        RemoveHandler editForm.PreviewRequested, AddressOf EditForm_PreviewRequested

        ' If we've already navigated away from this instance (shouldn't normally happen for a
        ' real close, but stay defensive), don't fight whatever's now showing.
        If Not ReferenceEquals(_activeContentForm, editForm) Then Return

        Dim wasSaved = editForm.DialogResult = DialogResult.OK
        ShowListView()
        If wasSaved Then LoadAll()
    End Sub

    Private Sub EditForm_PreviewRequested(sender As Object, e As EventArgs)
        Dim editForm = DirectCast(sender, FormNomenclature)
        Dim snapshot = editForm.BuildPreviewSnapshot()

        _suspendedEditForm = editForm
        _suspendedEditSubtitle = lblHeaderSubtitle.Text

        OpenPreview(snapshot, snapshot.Lignes)
    End Sub

    ' --- Preview (Aperçu) ---------------------------------------------------------------------

    Private Sub btnApercu_Click(sender As Object, e As EventArgs) Handles btnApercu.Click
        Dim selected = SelectedNomenclature()
        If selected Is Nothing Then Return

        Try
            ' The grid row only carries header fields - fetch the full, current record
            ' (with its Lignes) before previewing.
            Dim nomenclature = _repository.GetByCode(selected.Code)
            If nomenclature Is Nothing Then nomenclature = selected

            OpenPreview(nomenclature, nomenclature.Lignes)
        Catch ex As DataAccessException
            ShowDataError(ex)
        End Try
    End Sub

    Private Sub OpenPreview(nomenclature As Nomenclature, lignes As List(Of LigneNomenclature))
        Dim previewForm As New FormApercu(nomenclature, lignes)
        AddHandler previewForm.FormClosed, AddressOf PreviewForm_FormClosed
        AddHandler previewForm.ModifierRequested, AddressOf PreviewForm_ModifierRequested

        ShowEmbedded(previewForm, $"BikeSpec » Aperçu · {nomenclature.Code}")
    End Sub

    Private Sub PreviewForm_FormClosed(sender As Object, e As FormClosedEventArgs)
        Dim previewForm = DirectCast(sender, FormApercu)
        RemoveHandler previewForm.FormClosed, AddressOf PreviewForm_FormClosed
        RemoveHandler previewForm.ModifierRequested, AddressOf PreviewForm_ModifierRequested

        If Not ReferenceEquals(_activeContentForm, previewForm) Then Return ' already navigated away via Modifier

        DiscardSuspendedEditAndShowList()
    End Sub

    Private Sub PreviewForm_ModifierRequested(sender As Object, e As EventArgs)
        Dim previewForm = DirectCast(sender, FormApercu)

        If _suspendedEditForm IsNot Nothing Then
            ResumeSuspendedEditOrShowList()
        Else
            ' Reached Aperçu directly from the list (no in-progress edit to resume) - open a
            ' fresh editor for this record.
            OpenEditor(previewForm.Nomenclature)
        End If
    End Sub

    Private Sub ResumeSuspendedEditOrShowList()
        If _suspendedEditForm IsNot Nothing Then
            Dim resumed = _suspendedEditForm
            Dim subtitle = _suspendedEditSubtitle
            _suspendedEditForm = Nothing
            ShowEmbedded(resumed, subtitle)
        Else
            ShowListView()
        End If
    End Sub

    ''' <summary>Fermer always means "back to the list", even when the Aperçu was reached from an
    ''' in-progress edit (Modifier -> Aperçu). If a suspended edit session is behind this preview,
    ''' confirm before discarding its unsaved changes (the same prompt Annuler uses) rather than
    ''' silently resuming it.</summary>
    Private Sub DiscardSuspendedEditAndShowList()
        If _suspendedEditForm Is Nothing Then
            ShowListView()
            Return
        End If

        Dim suspended = _suspendedEditForm
        Dim subtitle = _suspendedEditSubtitle
        _suspendedEditForm = Nothing

        If suspended.ConfirmDiscard() Then
            suspended.Dispose()
            ShowListView()
        Else
            ' user chose to keep the unsaved changes - fall back to showing the edit form
            ShowEmbedded(suspended, subtitle)
        End If
    End Sub

End Class
