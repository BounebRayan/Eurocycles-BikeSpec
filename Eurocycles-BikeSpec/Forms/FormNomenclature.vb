Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Linq

Public Class FormNomenclature

    Private ReadOnly _isNewMode As Boolean
    Private ReadOnly _nomenclature As Nomenclature
    Private ReadOnly _repository As New NomenclatureRepository()
    Private _lignes As BindingList(Of LigneNomenclature)
    Private _isDirty As Boolean

    ''' <summary>Raised when the user clicks Aperçu: the host (FormListe) is responsible for
    ''' building/showing the preview - this form is only ever embedded, never opens another
    ''' top-level window itself.</summary>
    Public Event PreviewRequested As EventHandler

    Public Sub New()
        InitializeComponent()
        _isNewMode = True
        _nomenclature = New Nomenclature With {.Date = DateTime.Today}
        Me.Text = "Nouvelle nomenclature"
        btnApercu.Visible = False ' nothing meaningful to preview before the record exists
    End Sub

    Public Sub New(nomenclature As Nomenclature)
        InitializeComponent()
        If nomenclature Is Nothing Then Throw New ArgumentNullException(NameOf(nomenclature))
        _isNewMode = False
        _nomenclature = CloneNomenclature(nomenclature)
        Me.Text = $"Modifier nomenclature - {nomenclature.Code}"
    End Sub

    Private Shared Function CloneNomenclature(source As Nomenclature) As Nomenclature
        Return New Nomenclature With {
            .Code = source.Code,
            .Nom = source.Nom,
            .Date = source.Date,
            .Marque = source.Marque,
            .GenCode = source.GenCode,
            .NW = source.NW,
            .GW = source.GW,
            .Modele = source.Modele,
            .FrameSize = source.FrameSize,
            .WheelSize = source.WheelSize,
            .RefCustomer = source.RefCustomer,
            .Couleur = source.Couleur,
            .TypeDecor = source.TypeDecor,
            .Photo = If(source.Photo Is Nothing, Nothing, CType(source.Photo.Clone(), Byte()))
        }
    End Function

    Private Sub FormNomenclature_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateForm()

        Dim lines As List(Of LigneNomenclature) = Nothing
        If _isNewMode Then
            lines = New List(Of LigneNomenclature)()
        Else
            Try
                lines = _repository.GetByCode(_nomenclature.Code)?.Lignes
            Catch ex As DataAccessException
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            If lines Is Nothing Then lines = New List(Of LigneNomenclature)()
        End If

        _lignes = New BindingList(Of LigneNomenclature)(lines)
        dgvLignes.DataSource = _lignes
        RefreshTotals()

        ' Wired after PopulateForm()/lines are bound so initial population never marks the form dirty.
        WireDirtyTracking()
    End Sub

    ''' <summary>
    ''' Marks the form dirty on any header-field edit, so FormClosing can warn before
    ''' discarding unsaved changes. Line/photo edits mark it dirty at their own call sites.
    ''' </summary>
    Private Sub WireDirtyTracking()
        For Each ctrl As Control In New Control() {
            txtCode, txtNom, txtMarque, txtGenCode, txtNW, txtGW, txtModele, txtRefCustomer, txtCouleur
        }
            AddHandler ctrl.TextChanged, AddressOf MarkDirty
        Next
        AddHandler dtpDate.ValueChanged, AddressOf MarkDirty
        AddHandler cboFrameSize.SelectedIndexChanged, AddressOf MarkDirty
        AddHandler cboWheelSize.SelectedIndexChanged, AddressOf MarkDirty
        AddHandler cboTypeDecor.SelectedIndexChanged, AddressOf MarkDirty
    End Sub

    Private Sub MarkDirty(sender As Object, e As EventArgs)
        _isDirty = True
    End Sub

    Private Sub RefreshTotals()
        lblTotaux.Text = LigneTotalsCalculator.FormatTotals(_lignes)
        lblLignesTitle.Text = $"Lignes de la nomenclature ({_lignes.Count})"
    End Sub

    Private Sub dgvLignes_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLignes.CellEndEdit
        _isDirty = True
        RefreshTotals()
    End Sub

    Private Sub FormNomenclature_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult = DialogResult.OK Then Return ' saved successfully, nothing to warn about
        If Not ConfirmDiscard() Then e.Cancel = True
    End Sub

    ''' <summary>Returns True if it's fine to throw away this form's current state: either there's
    ''' nothing unsaved, or the user confirmed discarding it. Shared by the normal FormClosing
    ''' prompt and by the host (FormListe), which needs the same confirmation when discarding a
    ''' suspended edit session (detached but not yet closed) instead of resuming it.</summary>
    Public Function ConfirmDiscard() As Boolean
        If Not _isDirty Then Return True

        Dim confirm = MessageBox.Show(
            "Des modifications n'ont pas été enregistrées. Voulez-vous vraiment fermer sans enregistrer ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        Return confirm = DialogResult.Yes
    End Function

    Private Sub PopulateForm()
        txtCode.Text = _nomenclature.Code
        txtCode.ReadOnly = Not _isNewMode
        If txtCode.ReadOnly Then txtCode.BackColor = SystemColors.Control

        txtNom.Text = _nomenclature.Nom
        dtpDate.Value = If(_isNewMode, DateTime.Today, _nomenclature.Date)
        txtMarque.Text = _nomenclature.Marque
        txtGenCode.Text = _nomenclature.GenCode
        txtNW.Text = If(_nomenclature.NW.HasValue, _nomenclature.NW.Value.ToString(CultureInfo.CurrentCulture), String.Empty)
        txtGW.Text = If(_nomenclature.GW.HasValue, _nomenclature.GW.Value.ToString(CultureInfo.CurrentCulture), String.Empty)
        txtModele.Text = _nomenclature.Modele
        cboFrameSize.Text = If(_nomenclature.FrameSize, String.Empty)
        cboWheelSize.Text = If(_nomenclature.WheelSize, String.Empty)
        txtRefCustomer.Text = _nomenclature.RefCustomer
        txtCouleur.Text = _nomenclature.Couleur
        cboTypeDecor.Text = If(_nomenclature.TypeDecor, String.Empty)

        UpdatePhotoPreview()
    End Sub

    Private Sub UpdatePhotoPreview()
        Dim oldImage = picPhoto.Image
        picPhoto.Image = PhotoHelper.TryLoadImage(_nomenclature.Photo)
        oldImage?.Dispose()
    End Sub

    Private Sub btnChargerPhoto_Click(sender As Object, e As EventArgs) Handles btnChargerPhoto.Click
        Using dialog As New OpenFileDialog()
            dialog.Title = "Sélectionner une photo"
            dialog.Filter = "Images (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|Tous les fichiers (*.*)|*.*"

            If dialog.ShowDialog(Me) <> DialogResult.OK Then Return

            Dim bytes As Byte()
            Try
                bytes = File.ReadAllBytes(dialog.FileName)
            Catch ex As Exception
                MessageBox.Show($"Impossible de lire le fichier sélectionné : {ex.Message}",
                                 "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try

            If PhotoHelper.TryLoadImage(bytes) Is Nothing Then
                MessageBox.Show("Le fichier sélectionné n'est pas une image valide.",
                                 "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            _nomenclature.Photo = bytes
            UpdatePhotoPreview()
            _isDirty = True
        End Using
    End Sub

    Private Sub btnSupprimerPhoto_Click(sender As Object, e As EventArgs) Handles btnSupprimerPhoto.Click
        _nomenclature.Photo = Nothing
        UpdatePhotoPreview()
        _isDirty = True
    End Sub

    Private Sub btnAjouterLigne_Click(sender As Object, e As EventArgs) Handles btnAjouterLigne.Click
        Dim newLine As New LigneNomenclature With {
            .Code = LigneCodeGenerator.NewCode(),
            .NomenclatureCode = _nomenclature.Code,
            .Designation = String.Empty,
            .Quantite = 1D,
            .Prix = 0D,
            .Devise = "Euro",
            .Imprime = False
        }
        _lignes.Add(newLine)
        dgvLignes.CurrentCell = dgvLignes.Rows(dgvLignes.Rows.Count - 1).Cells("colDesignation")
        dgvLignes.BeginEdit(True)
        _isDirty = True
        RefreshTotals()
    End Sub

    Private Sub btnSupprimerLigne_Click(sender As Object, e As EventArgs) Handles btnSupprimerLigne.Click
        Dim line = TryCast(dgvLignes.CurrentRow?.DataBoundItem, LigneNomenclature)
        If line Is Nothing Then Return

        Dim confirm = MessageBox.Show(
            "Supprimer cette ligne ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)
        If confirm <> DialogResult.Yes Then Return

        _lignes.Remove(line)
        _isDirty = True
        RefreshTotals()
    End Sub

    Private Sub dgvLignes_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvLignes.DataError
        MessageBox.Show("Valeur invalide dans la ligne.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        e.ThrowException = False
    End Sub

    Private Sub txtGenCode_Leave(sender As Object, e As EventArgs) Handles txtGenCode.Leave
        ValidateGenCode()
    End Sub

    Private Sub txtNW_Leave(sender As Object, e As EventArgs) Handles txtNW.Leave
        ValidateDecimalField(txtNW, "NW")
    End Sub

    Private Sub txtGW_Leave(sender As Object, e As EventArgs) Handles txtGW.Leave
        ValidateDecimalField(txtGW, "GW")
    End Sub

    Private Function ValidateGenCode() As Boolean
        If GenCodeValidator.IsValid(txtGenCode.Text.Trim()) Then
            errorProvider.SetError(txtGenCode, String.Empty)
            Return True
        End If

        errorProvider.SetError(txtGenCode, "Le code générique doit contenir exactement 13 chiffres.")
        Return False
    End Function

    Private Function ValidateDecimalField(textBox As TextBox, fieldLabel As String) As Boolean
        Dim value = textBox.Text.Trim()
        If value.Length = 0 Then
            errorProvider.SetError(textBox, String.Empty)
            Return True
        End If

        Dim parsed As Decimal
        If Decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, parsed) Then
            errorProvider.SetError(textBox, String.Empty)
            Return True
        End If

        errorProvider.SetError(textBox, $"{fieldLabel} doit être un nombre décimal valide.")
        Return False
    End Function

    Private Function ValidateForm() As Boolean
        Dim isValid = True
        errorProvider.Clear()

        If String.IsNullOrWhiteSpace(txtCode.Text) Then
            errorProvider.SetError(txtCode, "Le code est obligatoire.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtNom.Text) Then
            errorProvider.SetError(txtNom, "Le nom est obligatoire.")
            isValid = False
        End If

        If Not ValidateGenCode() Then isValid = False
        If Not ValidateDecimalField(txtNW, "NW") Then isValid = False
        If Not ValidateDecimalField(txtGW, "GW") Then isValid = False

        Return isValid
    End Function

    ''' <summary>
    ''' Validates every ligne (Code required+unique, Designation required, Quantite > 0,
    ''' Prix >= 0, Devise selected), highlighting failures on the grid cells themselves
    ''' via DataGridViewCell.ErrorText rather than a single generic message.
    ''' </summary>
    Private Function ValidateLignes() As Boolean
        Dim isValid = True
        ClearLigneErrors()

        For i = 0 To _lignes.Count - 1
            Dim line = _lignes(i)
            Dim row = dgvLignes.Rows(i)

            If String.IsNullOrWhiteSpace(line.Code) Then
                row.Cells("colCode").ErrorText = "Le code est obligatoire."
                isValid = False
            ElseIf _lignes.AsEnumerable().Count(Function(l) l.Code = line.Code) > 1 Then
                row.Cells("colCode").ErrorText = "Le code doit être unique."
                isValid = False
            End If

            If String.IsNullOrWhiteSpace(line.Designation) Then
                row.Cells("colDesignation").ErrorText = "La désignation est obligatoire."
                isValid = False
            End If

            If line.Quantite <= 0 Then
                row.Cells("colQuantite").ErrorText = "La quantité doit être supérieure à 0."
                isValid = False
            End If

            If line.Prix < 0 Then
                row.Cells("colPrix").ErrorText = "Le prix ne peut pas être négatif."
                isValid = False
            End If

            If String.IsNullOrWhiteSpace(line.Devise) Then
                row.Cells("colDevise").ErrorText = "La devise est obligatoire."
                isValid = False
            End If
        Next

        Return isValid
    End Function

    Private Sub ClearLigneErrors()
        For Each row As DataGridViewRow In dgvLignes.Rows
            For Each cell As DataGridViewCell In row.Cells
                cell.ErrorText = String.Empty
            Next
        Next
    End Sub

    Private Sub ApplyFormToNomenclature()
        _nomenclature.Code = txtCode.Text.Trim()
        _nomenclature.Nom = txtNom.Text.Trim()
        _nomenclature.Date = dtpDate.Value.Date
        _nomenclature.Marque = NullableConverter.NullIfEmpty(txtMarque.Text)
        _nomenclature.GenCode = NullableConverter.NullIfEmpty(txtGenCode.Text)
        _nomenclature.NW = NullableConverter.ParseNullableDecimal(txtNW.Text)
        _nomenclature.GW = NullableConverter.ParseNullableDecimal(txtGW.Text)
        _nomenclature.Modele = NullableConverter.NullIfEmpty(txtModele.Text)
        _nomenclature.FrameSize = NullableConverter.NullIfEmpty(cboFrameSize.Text)
        _nomenclature.WheelSize = NullableConverter.NullIfEmpty(cboWheelSize.Text)
        _nomenclature.RefCustomer = NullableConverter.NullIfEmpty(txtRefCustomer.Text)
        _nomenclature.Couleur = NullableConverter.NullIfEmpty(txtCouleur.Text)
        _nomenclature.TypeDecor = NullableConverter.NullIfEmpty(cboTypeDecor.Text)
    End Sub

    Private Sub btnEnregistrer_Click(sender As Object, e As EventArgs) Handles btnEnregistrer.Click
        dgvLignes.EndEdit()

        ' Evaluate both unconditionally so header AND ligne errors are all shown together,
        ' not just whichever fails first.
        Dim headerValid = ValidateForm()
        Dim lignesValid = ValidateLignes()
        If Not headerValid OrElse Not lignesValid Then Return

        ApplyFormToNomenclature()
        _nomenclature.Lignes = _lignes.ToList()

        Try
            If _isNewMode Then
                _repository.Insert(_nomenclature)
            Else
                _repository.Update(_nomenclature)
            End If
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As DataAccessException
            MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>Builds an up-to-date snapshot from whatever is currently entered (even if not
    ''' yet saved) for the host to hand to FormApercu. Public because FormListe calls it in response
    ''' to PreviewRequested; this form never creates FormApercu itself.</summary>
    Public Function BuildPreviewSnapshot() As Nomenclature
        dgvLignes.EndEdit()
        ApplyFormToNomenclature()
        _nomenclature.Lignes = _lignes.ToList()
        Return _nomenclature
    End Function

    Private Sub btnApercu_Click(sender As Object, e As EventArgs) Handles btnApercu.Click
        RaiseEvent PreviewRequested(Me, EventArgs.Empty)
    End Sub

End Class
