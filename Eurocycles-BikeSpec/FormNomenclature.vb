Imports System.Globalization
Imports System.IO
Imports System.Linq

Public Class FormNomenclature

    Private ReadOnly _isNewMode As Boolean
    Private ReadOnly _nomenclature As Nomenclature
    Private ReadOnly _ligneRepository As New LigneNomenclatureRepository()
    Private ReadOnly _editorService As New NomenclatureEditorService()
    Private _originalLineCodes As List(Of String)

    Public Sub New()
        InitializeComponent()
        _isNewMode = True
        _nomenclature = New Nomenclature With {.Date = DateTime.Today}
        Me.Text = "Nouvelle nomenclature"
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

        Dim lines As List(Of LigneNomenclature)
        If _isNewMode Then
            lines = New List(Of LigneNomenclature)()
            _originalLineCodes = New List(Of String)()
        Else
            Try
                lines = _ligneRepository.GetByNomenclatureCode(_nomenclature.Code)
            Catch ex As DataAccessException
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                lines = New List(Of LigneNomenclature)()
            End Try
            _originalLineCodes = lines.Select(Function(l) l.Code).ToList()
        End If

        bsLignes.DataSource = lines
        dgvLignes.DataSource = bsLignes
    End Sub

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
        picPhoto.Image = Nothing
        oldImage?.Dispose()

        If _nomenclature.Photo IsNot Nothing AndAlso _nomenclature.Photo.Length > 0 Then
            Using ms As New MemoryStream(_nomenclature.Photo)
                Using loaded = Image.FromStream(ms)
                    picPhoto.Image = New Bitmap(loaded)
                End Using
            End Using
        End If
    End Sub

    Private Sub btnChargerPhoto_Click(sender As Object, e As EventArgs) Handles btnChargerPhoto.Click
        Using dialog As New OpenFileDialog()
            dialog.Title = "Sélectionner une photo"
            dialog.Filter = "Images (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|Tous les fichiers (*.*)|*.*"

            If dialog.ShowDialog(Me) = DialogResult.OK Then
                _nomenclature.Photo = File.ReadAllBytes(dialog.FileName)
                UpdatePhotoPreview()
            End If
        End Using
    End Sub

    Private Sub btnSupprimerPhoto_Click(sender As Object, e As EventArgs) Handles btnSupprimerPhoto.Click
        _nomenclature.Photo = Nothing
        UpdatePhotoPreview()
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
        bsLignes.Add(newLine)
        dgvLignes.CurrentCell = dgvLignes.Rows(dgvLignes.Rows.Count - 1).Cells("colDesignation")
        dgvLignes.BeginEdit(True)
    End Sub

    Private Sub btnSupprimerLigne_Click(sender As Object, e As EventArgs) Handles btnSupprimerLigne.Click
        Dim line = TryCast(dgvLignes.CurrentRow?.DataBoundItem, LigneNomenclature)
        If line Is Nothing Then Return
        bsLignes.Remove(line)
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

    Private Sub ApplyFormToNomenclature()
        _nomenclature.Code = txtCode.Text.Trim()
        _nomenclature.Nom = txtNom.Text.Trim()
        _nomenclature.Date = dtpDate.Value.Date
        _nomenclature.Marque = NullIfEmpty(txtMarque.Text)
        _nomenclature.GenCode = NullIfEmpty(txtGenCode.Text)
        _nomenclature.NW = ParseNullableDecimal(txtNW.Text)
        _nomenclature.GW = ParseNullableDecimal(txtGW.Text)
        _nomenclature.Modele = NullIfEmpty(txtModele.Text)
        _nomenclature.FrameSize = NullIfEmpty(cboFrameSize.Text)
        _nomenclature.WheelSize = NullIfEmpty(cboWheelSize.Text)
        _nomenclature.RefCustomer = NullIfEmpty(txtRefCustomer.Text)
        _nomenclature.Couleur = NullIfEmpty(txtCouleur.Text)
        _nomenclature.TypeDecor = NullIfEmpty(cboTypeDecor.Text)
    End Sub

    Private Shared Function NullIfEmpty(value As String) As String
        Dim trimmed = value?.Trim()
        Return If(String.IsNullOrEmpty(trimmed), Nothing, trimmed)
    End Function

    Private Shared Function ParseNullableDecimal(text As String) As Decimal?
        Dim trimmed = text?.Trim()
        If String.IsNullOrEmpty(trimmed) Then Return Nothing
        Dim result As Decimal
        If Decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.CurrentCulture, result) Then
            Return result
        End If
        Return Nothing
    End Function

    Private Sub btnEnregistrer_Click(sender As Object, e As EventArgs) Handles btnEnregistrer.Click
        dgvLignes.EndEdit()
        bsLignes.EndEdit()

        If Not ValidateForm() Then Return

        ApplyFormToNomenclature()

        Dim lines = bsLignes.List.Cast(Of LigneNomenclature)().ToList()
        For Each line In lines
            line.NomenclatureCode = _nomenclature.Code
        Next

        Try
            _editorService.Save(_nomenclature, lines, _originalLineCodes, _isNewMode)
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

End Class
