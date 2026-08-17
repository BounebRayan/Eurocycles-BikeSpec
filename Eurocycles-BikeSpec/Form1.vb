Public Class Form1

    Private ReadOnly _repository As New NomenclatureRepository()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvNomenclatures.DataSource = bsNomenclatures
        LoadAll()
    End Sub

    Private Sub LoadAll()
        Try
            bsNomenclatures.DataSource = _repository.GetAll()
        Catch ex As DataAccessException
            ShowDataError(ex)
        End Try
    End Sub

    Private Sub PerformSearch()
        Dim term = txtSearch.Text.Trim()
        Try
            bsNomenclatures.DataSource = If(term.Length = 0, _repository.GetAll(), _repository.Search(term))
        Catch ex As DataAccessException
            ShowDataError(ex)
        End Try
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
        LoadAll()
    End Sub

    Private Sub btnNouveau_Click(sender As Object, e As EventArgs) Handles btnNouveau.Click
        Using form As New FormNomenclature()
            If form.ShowDialog(Me) = DialogResult.OK Then
                LoadAll()
            End If
        End Using
    End Sub

    Private Sub btnModifier_Click(sender As Object, e As EventArgs) Handles btnModifier.Click
        OpenEditForSelected()
    End Sub

    Private Sub dgvNomenclatures_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvNomenclatures.CellDoubleClick
        If e.RowIndex >= 0 Then
            OpenEditForSelected()
        End If
    End Sub

    Private Sub OpenEditForSelected()
        Dim selected = SelectedNomenclature()
        If selected Is Nothing Then Return

        Using form As New FormNomenclature(selected)
            If form.ShowDialog(Me) = DialogResult.OK Then
                LoadAll()
            End If
        End Using
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

    Private Sub btnApercu_Click(sender As Object, e As EventArgs) Handles btnApercu.Click
        If SelectedNomenclature() Is Nothing Then Return
        MessageBox.Show("Aperçu à venir", "Aperçu", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

End Class
