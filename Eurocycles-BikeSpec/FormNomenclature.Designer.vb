<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormNomenclature
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()

        Me.errorProvider = New ErrorProvider(Me.components)

        Me.pnlHeader = New Panel()
        Me.tlpHeader = New TableLayoutPanel()
        Me.lblCode = New Label()
        Me.txtCode = New TextBox()
        Me.lblNom = New Label()
        Me.txtNom = New TextBox()
        Me.lblDate = New Label()
        Me.dtpDate = New DateTimePicker()
        Me.lblMarque = New Label()
        Me.txtMarque = New TextBox()
        Me.lblGenCode = New Label()
        Me.txtGenCode = New TextBox()
        Me.lblNW = New Label()
        Me.txtNW = New TextBox()
        Me.lblGW = New Label()
        Me.txtGW = New TextBox()
        Me.lblModele = New Label()
        Me.txtModele = New TextBox()
        Me.lblFrameSize = New Label()
        Me.cboFrameSize = New ComboBox()
        Me.lblWheelSize = New Label()
        Me.cboWheelSize = New ComboBox()
        Me.lblRefCustomer = New Label()
        Me.txtRefCustomer = New TextBox()
        Me.lblCouleur = New Label()
        Me.txtCouleur = New TextBox()
        Me.lblTypeDecor = New Label()
        Me.cboTypeDecor = New ComboBox()

        Me.grpPhoto = New GroupBox()
        Me.picPhoto = New PictureBox()
        Me.btnChargerPhoto = New Button()
        Me.btnSupprimerPhoto = New Button()

        Me.grpLignes = New GroupBox()
        Me.pnlLignesButtons = New FlowLayoutPanel()
        Me.btnAjouterLigne = New Button()
        Me.btnSupprimerLigne = New Button()
        Me.dgvLignes = New DataGridView()
        Me.pnlTotaux = New Panel()
        Me.lblTotaux = New Label()

        Me.pnlBottom = New FlowLayoutPanel()
        Me.btnEnregistrer = New Button()
        Me.btnAnnuler = New Button()
        Me.btnApercu = New Button()

        CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLignes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.tlpHeader.SuspendLayout()
        Me.grpPhoto.SuspendLayout()
        Me.grpLignes.SuspendLayout()
        Me.pnlLignesButtons.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()

        ' errorProvider
        Me.errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink

        ' pnlHeader
        Me.pnlHeader.Dock = DockStyle.Top
        Me.pnlHeader.Height = 250
        Me.pnlHeader.Controls.Add(Me.tlpHeader)
        Me.pnlHeader.Controls.Add(Me.grpPhoto)

        ' tlpHeader
        Me.tlpHeader.Dock = DockStyle.Left
        Me.tlpHeader.Width = 630
        Me.tlpHeader.Padding = New Padding(10)
        Me.tlpHeader.ColumnCount = 4
        Me.tlpHeader.RowCount = 7
        Me.tlpHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 110))
        Me.tlpHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 200))
        Me.tlpHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 110))
        Me.tlpHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 200))
        For i = 0 To 6
            Me.tlpHeader.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F / 7.0F))
        Next

        ' Row 0: Code / Nom
        Me.lblCode.Text = "Code *"
        Me.lblCode.Dock = DockStyle.Fill
        Me.lblCode.TextAlign = ContentAlignment.MiddleLeft
        Me.txtCode.Dock = DockStyle.Fill
        Me.txtCode.MaxLength = 20
        Me.lblNom.Text = "Nom *"
        Me.lblNom.Dock = DockStyle.Fill
        Me.lblNom.TextAlign = ContentAlignment.MiddleLeft
        Me.txtNom.Dock = DockStyle.Fill
        Me.txtNom.MaxLength = 100

        ' Row 1: Date / Marque
        Me.lblDate.Text = "Date *"
        Me.lblDate.Dock = DockStyle.Fill
        Me.lblDate.TextAlign = ContentAlignment.MiddleLeft
        Me.dtpDate.Dock = DockStyle.Fill
        Me.dtpDate.Format = DateTimePickerFormat.Short
        Me.lblMarque.Text = "Marque"
        Me.lblMarque.Dock = DockStyle.Fill
        Me.lblMarque.TextAlign = ContentAlignment.MiddleLeft
        Me.txtMarque.Dock = DockStyle.Fill
        Me.txtMarque.MaxLength = 50

        ' Row 2: GenCode / NW
        Me.lblGenCode.Text = "GenCode"
        Me.lblGenCode.Dock = DockStyle.Fill
        Me.lblGenCode.TextAlign = ContentAlignment.MiddleLeft
        Me.txtGenCode.Dock = DockStyle.Fill
        Me.txtGenCode.MaxLength = 13
        Me.lblNW.Text = "NW (kg)"
        Me.lblNW.Dock = DockStyle.Fill
        Me.lblNW.TextAlign = ContentAlignment.MiddleLeft
        Me.txtNW.Dock = DockStyle.Fill

        ' Row 3: GW / Modele
        Me.lblGW.Text = "GW (kg)"
        Me.lblGW.Dock = DockStyle.Fill
        Me.lblGW.TextAlign = ContentAlignment.MiddleLeft
        Me.txtGW.Dock = DockStyle.Fill
        Me.lblModele.Text = "Modèle"
        Me.lblModele.Dock = DockStyle.Fill
        Me.lblModele.TextAlign = ContentAlignment.MiddleLeft
        Me.txtModele.Dock = DockStyle.Fill
        Me.txtModele.MaxLength = 50

        ' Row 4: FrameSize / WheelSize
        Me.lblFrameSize.Text = "Taille cadre"
        Me.lblFrameSize.Dock = DockStyle.Fill
        Me.lblFrameSize.TextAlign = ContentAlignment.MiddleLeft
        Me.cboFrameSize.Dock = DockStyle.Fill
        Me.cboFrameSize.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboFrameSize.Items.Add("")
        Me.cboFrameSize.Items.AddRange(AllowedValues.FrameSizes)
        Me.lblWheelSize.Text = "Taille roue"
        Me.lblWheelSize.Dock = DockStyle.Fill
        Me.lblWheelSize.TextAlign = ContentAlignment.MiddleLeft
        Me.cboWheelSize.Dock = DockStyle.Fill
        Me.cboWheelSize.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboWheelSize.Items.Add("")
        Me.cboWheelSize.Items.AddRange(AllowedValues.WheelSizes)

        ' Row 5: RefCustomer / Couleur
        Me.lblRefCustomer.Text = "Réf. client"
        Me.lblRefCustomer.Dock = DockStyle.Fill
        Me.lblRefCustomer.TextAlign = ContentAlignment.MiddleLeft
        Me.txtRefCustomer.Dock = DockStyle.Fill
        Me.txtRefCustomer.MaxLength = 50
        Me.lblCouleur.Text = "Couleur"
        Me.lblCouleur.Dock = DockStyle.Fill
        Me.lblCouleur.TextAlign = ContentAlignment.MiddleLeft
        Me.txtCouleur.Dock = DockStyle.Fill
        Me.txtCouleur.MaxLength = 50

        ' Row 6: TypeDecor
        Me.lblTypeDecor.Text = "Type décor"
        Me.lblTypeDecor.Dock = DockStyle.Fill
        Me.lblTypeDecor.TextAlign = ContentAlignment.MiddleLeft
        Me.cboTypeDecor.Dock = DockStyle.Fill
        Me.cboTypeDecor.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboTypeDecor.Items.Add("")
        Me.cboTypeDecor.Items.AddRange(AllowedValues.TypeDecors)

        Me.tlpHeader.Controls.Add(Me.lblCode, 0, 0)
        Me.tlpHeader.Controls.Add(Me.txtCode, 1, 0)
        Me.tlpHeader.Controls.Add(Me.lblNom, 2, 0)
        Me.tlpHeader.Controls.Add(Me.txtNom, 3, 0)
        Me.tlpHeader.Controls.Add(Me.lblDate, 0, 1)
        Me.tlpHeader.Controls.Add(Me.dtpDate, 1, 1)
        Me.tlpHeader.Controls.Add(Me.lblMarque, 2, 1)
        Me.tlpHeader.Controls.Add(Me.txtMarque, 3, 1)
        Me.tlpHeader.Controls.Add(Me.lblGenCode, 0, 2)
        Me.tlpHeader.Controls.Add(Me.txtGenCode, 1, 2)
        Me.tlpHeader.Controls.Add(Me.lblNW, 2, 2)
        Me.tlpHeader.Controls.Add(Me.txtNW, 3, 2)
        Me.tlpHeader.Controls.Add(Me.lblGW, 0, 3)
        Me.tlpHeader.Controls.Add(Me.txtGW, 1, 3)
        Me.tlpHeader.Controls.Add(Me.lblModele, 2, 3)
        Me.tlpHeader.Controls.Add(Me.txtModele, 3, 3)
        Me.tlpHeader.Controls.Add(Me.lblFrameSize, 0, 4)
        Me.tlpHeader.Controls.Add(Me.cboFrameSize, 1, 4)
        Me.tlpHeader.Controls.Add(Me.lblWheelSize, 2, 4)
        Me.tlpHeader.Controls.Add(Me.cboWheelSize, 3, 4)
        Me.tlpHeader.Controls.Add(Me.lblRefCustomer, 0, 5)
        Me.tlpHeader.Controls.Add(Me.txtRefCustomer, 1, 5)
        Me.tlpHeader.Controls.Add(Me.lblCouleur, 2, 5)
        Me.tlpHeader.Controls.Add(Me.txtCouleur, 3, 5)
        Me.tlpHeader.Controls.Add(Me.lblTypeDecor, 0, 6)
        Me.tlpHeader.Controls.Add(Me.cboTypeDecor, 1, 6)

        ' grpPhoto
        Me.grpPhoto.Dock = DockStyle.Right
        Me.grpPhoto.Width = 200
        Me.grpPhoto.Text = "Photo"
        Me.picPhoto.Location = New Point(15, 25)
        Me.picPhoto.Size = New Size(160, 120)
        Me.picPhoto.BorderStyle = BorderStyle.FixedSingle
        Me.picPhoto.SizeMode = PictureBoxSizeMode.Zoom
        Me.btnChargerPhoto.Location = New Point(15, 155)
        Me.btnChargerPhoto.Size = New Size(160, 28)
        Me.btnChargerPhoto.Text = "Choisir photo..."
        Me.btnSupprimerPhoto.Location = New Point(15, 189)
        Me.btnSupprimerPhoto.Size = New Size(160, 28)
        Me.btnSupprimerPhoto.Text = "Supprimer"
        Me.grpPhoto.Controls.Add(Me.picPhoto)
        Me.grpPhoto.Controls.Add(Me.btnChargerPhoto)
        Me.grpPhoto.Controls.Add(Me.btnSupprimerPhoto)

        ' grpLignes
        Me.grpLignes.Dock = DockStyle.Fill
        Me.grpLignes.Text = "Lignes de la nomenclature"
        Me.grpLignes.Controls.Add(Me.dgvLignes)
        Me.grpLignes.Controls.Add(Me.pnlTotaux)
        Me.grpLignes.Controls.Add(Me.pnlLignesButtons)

        ' pnlTotaux
        Me.pnlTotaux.Dock = DockStyle.Bottom
        Me.pnlTotaux.Height = 28
        Me.lblTotaux.Dock = DockStyle.Fill
        Me.lblTotaux.TextAlign = ContentAlignment.MiddleRight
        Me.lblTotaux.Font = New Font(Me.lblTotaux.Font, FontStyle.Bold)
        Me.lblTotaux.Padding = New Padding(0, 0, 10, 0)
        Me.pnlTotaux.Controls.Add(Me.lblTotaux)

        ' pnlLignesButtons
        Me.pnlLignesButtons.Dock = DockStyle.Top
        Me.pnlLignesButtons.AutoSize = True
        Me.pnlLignesButtons.FlowDirection = FlowDirection.LeftToRight
        Me.pnlLignesButtons.Padding = New Padding(5)
        Me.btnAjouterLigne.AutoSize = True
        Me.btnAjouterLigne.Text = "Ajouter ligne"
        Me.btnAjouterLigne.Margin = New Padding(3)
        Me.btnSupprimerLigne.AutoSize = True
        Me.btnSupprimerLigne.Text = "Supprimer ligne"
        Me.btnSupprimerLigne.Margin = New Padding(3)
        Me.pnlLignesButtons.Controls.Add(Me.btnAjouterLigne)
        Me.pnlLignesButtons.Controls.Add(Me.btnSupprimerLigne)

        ' dgvLignes
        Me.dgvLignes.Dock = DockStyle.Fill
        Me.dgvLignes.AllowUserToAddRows = False
        Me.dgvLignes.AllowUserToDeleteRows = False
        Me.dgvLignes.RowHeadersVisible = False
        Me.dgvLignes.MultiSelect = False
        Me.dgvLignes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvLignes.AutoGenerateColumns = False
        Me.dgvLignes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Dim colCode As New DataGridViewTextBoxColumn() With {
            .Name = "colCode", .HeaderText = "Code", .DataPropertyName = "Code", .FillWeight = 14
        }
        colCode.MaxInputLength = 20 ' matches LigneNomenclature.Code NVARCHAR(20)
        Dim colDesignation As New DataGridViewTextBoxColumn() With {
            .Name = "colDesignation", .HeaderText = "Désignation", .DataPropertyName = "Designation", .FillWeight = 22
        }
        colDesignation.MaxInputLength = 150
        Dim colQuantite As New DataGridViewTextBoxColumn() With {
            .Name = "colQuantite", .HeaderText = "Qté", .DataPropertyName = "Quantite", .FillWeight = 9
        }
        colQuantite.DefaultCellStyle.Format = "N2"
        colQuantite.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dim colPrix As New DataGridViewTextBoxColumn() With {
            .Name = "colPrix", .HeaderText = "Prix", .DataPropertyName = "Prix", .FillWeight = 11
        }
        colPrix.DefaultCellStyle.Format = "N3"
        colPrix.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dim colFabricant As New DataGridViewTextBoxColumn() With {
            .Name = "colFabricant", .HeaderText = "Fabricant", .DataPropertyName = "Fabricant", .FillWeight = 16
        }
        colFabricant.MaxInputLength = 100
        Dim colImprime As New DataGridViewCheckBoxColumn() With {
            .Name = "colImprime", .HeaderText = "Imprimé", .DataPropertyName = "Imprime", .FillWeight = 8
        }
        Dim colObservation As New DataGridViewTextBoxColumn() With {
            .Name = "colObservation", .HeaderText = "Observation", .DataPropertyName = "Observation", .FillWeight = 18
        }
        colObservation.MaxInputLength = 255
        Dim colDevise As New DataGridViewComboBoxColumn() With {
            .Name = "colDevise", .HeaderText = "Devise", .DataPropertyName = "Devise", .FillWeight = 8
        }
        colDevise.Items.AddRange(AllowedValues.Devises)

        Me.dgvLignes.Columns.AddRange(New DataGridViewColumn() {
            colCode, colDesignation, colQuantite, colPrix, colFabricant, colImprime, colObservation, colDevise
        })

        ' pnlBottom
        Me.pnlBottom.Dock = DockStyle.Bottom
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.FlowDirection = FlowDirection.RightToLeft
        Me.pnlBottom.Padding = New Padding(10)
        Me.btnEnregistrer.AutoSize = True
        Me.btnEnregistrer.Text = "Enregistrer"
        Me.btnEnregistrer.Margin = New Padding(3)
        Me.btnAnnuler.AutoSize = True
        Me.btnAnnuler.Text = "Annuler"
        Me.btnAnnuler.Margin = New Padding(3)
        Me.btnApercu.AutoSize = True
        Me.btnApercu.Text = "Aperçu"
        Me.btnApercu.Margin = New Padding(3)
        Me.pnlBottom.Controls.Add(Me.btnEnregistrer)
        Me.pnlBottom.Controls.Add(Me.btnAnnuler)
        Me.pnlBottom.Controls.Add(Me.btnApercu)

        ' FormNomenclature
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(1000, 700)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.AcceptButton = Me.btnEnregistrer
        Me.CancelButton = Me.btnAnnuler
        Me.Controls.Add(Me.grpLignes)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlHeader)

        CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLignes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.tlpHeader.ResumeLayout(False)
        Me.grpPhoto.ResumeLayout(False)
        Me.grpLignes.ResumeLayout(False)
        Me.pnlLignesButtons.ResumeLayout(False)
        Me.pnlLignesButtons.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents errorProvider As ErrorProvider
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents tlpHeader As TableLayoutPanel
    Friend WithEvents lblCode As Label
    Friend WithEvents txtCode As TextBox
    Friend WithEvents lblNom As Label
    Friend WithEvents txtNom As TextBox
    Friend WithEvents lblDate As Label
    Friend WithEvents dtpDate As DateTimePicker
    Friend WithEvents lblMarque As Label
    Friend WithEvents txtMarque As TextBox
    Friend WithEvents lblGenCode As Label
    Friend WithEvents txtGenCode As TextBox
    Friend WithEvents lblNW As Label
    Friend WithEvents txtNW As TextBox
    Friend WithEvents lblGW As Label
    Friend WithEvents txtGW As TextBox
    Friend WithEvents lblModele As Label
    Friend WithEvents txtModele As TextBox
    Friend WithEvents lblFrameSize As Label
    Friend WithEvents cboFrameSize As ComboBox
    Friend WithEvents lblWheelSize As Label
    Friend WithEvents cboWheelSize As ComboBox
    Friend WithEvents lblRefCustomer As Label
    Friend WithEvents txtRefCustomer As TextBox
    Friend WithEvents lblCouleur As Label
    Friend WithEvents txtCouleur As TextBox
    Friend WithEvents lblTypeDecor As Label
    Friend WithEvents cboTypeDecor As ComboBox
    Friend WithEvents grpPhoto As GroupBox
    Friend WithEvents picPhoto As PictureBox
    Friend WithEvents btnChargerPhoto As Button
    Friend WithEvents btnSupprimerPhoto As Button
    Friend WithEvents grpLignes As GroupBox
    Friend WithEvents pnlLignesButtons As FlowLayoutPanel
    Friend WithEvents btnAjouterLigne As Button
    Friend WithEvents btnSupprimerLigne As Button
    Friend WithEvents dgvLignes As DataGridView
    Friend WithEvents pnlTotaux As Panel
    Friend WithEvents lblTotaux As Label
    Friend WithEvents pnlBottom As FlowLayoutPanel
    Friend WithEvents btnEnregistrer As Button
    Friend WithEvents btnAnnuler As Button
    Friend WithEvents btnApercu As Button

End Class
