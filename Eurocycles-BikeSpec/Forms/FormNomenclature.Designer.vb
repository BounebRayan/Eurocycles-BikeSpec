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

        Me.pnlHeaderCards = New Panel()
        Me.cardIdentification = New Panel()
        Me.lblCardIdentification = New Label()
        Me.lblCode = New Label()
        Me.txtCode = New TextBox()
        Me.lblNom = New Label()
        Me.txtNom = New TextBox()
        Me.lblDate = New Label()
        Me.dtpDate = New DateTimePicker()
        Me.lblRefCustomer = New Label()
        Me.txtRefCustomer = New TextBox()

        Me.cardTechnique = New Panel()
        Me.lblCardTechnique = New Label()
        Me.lblMarque = New Label()
        Me.txtMarque = New TextBox()
        Me.lblModele = New Label()
        Me.txtModele = New TextBox()
        Me.lblGenCode = New Label()
        Me.txtGenCode = New TextBox()
        Me.lblCouleur = New Label()
        Me.txtCouleur = New TextBox()
        Me.lblNW = New Label()
        Me.txtNW = New TextBox()
        Me.lblGW = New Label()
        Me.txtGW = New TextBox()
        Me.lblFrameSize = New Label()
        Me.cboFrameSize = New ComboBox()
        Me.lblWheelSize = New Label()
        Me.cboWheelSize = New ComboBox()
        Me.lblTypeDecor = New Label()
        Me.cboTypeDecor = New ComboBox()

        Me.grpPhoto = New Panel()
        Me.lblCardPhoto = New Label()
        Me.picPhoto = New PictureBox()
        Me.btnChargerPhoto = New Button()
        Me.btnSupprimerPhoto = New Button()

        Me.grpLignes = New Panel()
        Me.pnlLignesHeader = New Panel()
        Me.lblLignesTitle = New Label()
        Me.pnlLignesButtons = New FlowLayoutPanel()
        Me.btnAjouterLigne = New Button()
        Me.btnSupprimerLigne = New Button()
        Me.dgvLignes = New DataGridView()

        Me.pnlBottom = New Panel()
        Me.lblTotaux = New Label()
        Me.pnlBottomButtons = New FlowLayoutPanel()
        Me.btnEnregistrer = New Button()
        Me.btnAnnuler = New Button()
        Me.btnApercu = New Button()

        CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLignes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeaderCards.SuspendLayout()
        Me.cardIdentification.SuspendLayout()
        Me.cardTechnique.SuspendLayout()
        Me.grpPhoto.SuspendLayout()
        Me.grpLignes.SuspendLayout()
        Me.pnlLignesHeader.SuspendLayout()
        Me.pnlLignesButtons.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.pnlBottomButtons.SuspendLayout()
        Me.SuspendLayout()

        ' errorProvider
        Me.errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink

        ' pnlHeaderCards (contains the 3 side-by-side cards)
        Me.pnlHeaderCards.Dock = DockStyle.Top
        Me.pnlHeaderCards.Height = 420
        Me.pnlHeaderCards.BackColor = Theme.CardBackground

        ' cardIdentification
        Me.cardIdentification.Location = New Point(22, 10)
        Me.cardIdentification.Size = New Size(340, 400)
        Theme.ApplyCardStyle(Me.cardIdentification)
        Me.lblCardIdentification.Text = "Identification"
        Me.lblCardIdentification.AutoSize = True
        Me.lblCardIdentification.Location = New Point(16, 14)
        Theme.ApplySectionHeader(Me.lblCardIdentification)
        Me.cardIdentification.Controls.Add(Me.lblCardIdentification)

        Dim yId = 44
        yId = Theme.AddField(Me.cardIdentification, Me.lblCode, "Code *", Me.txtCode, 16, yId, 304)
        yId = Theme.AddField(Me.cardIdentification, Me.lblNom, "Nom *", Me.txtNom, 16, yId, 304)
        yId = Theme.AddField(Me.cardIdentification, Me.lblDate, "Date *", Me.dtpDate, 16, yId, 304)
        yId = Theme.AddField(Me.cardIdentification, Me.lblRefCustomer, "Réf. client", Me.txtRefCustomer, 16, yId, 304)

        ' cardTechnique
        Me.cardTechnique.Location = New Point(380, 10)
        Me.cardTechnique.Size = New Size(340, 400)
        Theme.ApplyCardStyle(Me.cardTechnique)
        Me.lblCardTechnique.Text = "Caractéristiques techniques"
        Me.lblCardTechnique.AutoSize = True
        Me.lblCardTechnique.Location = New Point(16, 14)
        Theme.ApplySectionHeader(Me.lblCardTechnique)
        Me.cardTechnique.Controls.Add(Me.lblCardTechnique)

        Dim yTech = 44
        Theme.AddField(Me.cardTechnique, Me.lblMarque, "Marque", Me.txtMarque, 16, yTech, 142)
        yTech = Theme.AddField(Me.cardTechnique, Me.lblModele, "Modèle", Me.txtModele, 178, yTech, 142)
        Theme.AddField(Me.cardTechnique, Me.lblGenCode, "GenCode", Me.txtGenCode, 16, yTech, 142)
        yTech = Theme.AddField(Me.cardTechnique, Me.lblCouleur, "Couleur", Me.txtCouleur, 178, yTech, 142)
        Me.txtGenCode.MaxLength = 13
        Theme.AddField(Me.cardTechnique, Me.lblNW, "NW (kg)", Me.txtNW, 16, yTech, 142)
        yTech = Theme.AddField(Me.cardTechnique, Me.lblGW, "GW (kg)", Me.txtGW, 178, yTech, 142)
        Me.cboFrameSize.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboFrameSize.Items.Add("")
        Me.cboFrameSize.Items.AddRange(AllowedValues.FrameSizes)
        Theme.AddField(Me.cardTechnique, Me.lblFrameSize, "Taille cadre", Me.cboFrameSize, 16, yTech, 142)
        Me.cboWheelSize.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboWheelSize.Items.Add("")
        Me.cboWheelSize.Items.AddRange(AllowedValues.WheelSizes)
        yTech = Theme.AddField(Me.cardTechnique, Me.lblWheelSize, "Taille roue", Me.cboWheelSize, 178, yTech, 142)
        Me.cboTypeDecor.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboTypeDecor.Items.Add("")
        Me.cboTypeDecor.Items.AddRange(AllowedValues.TypeDecors)
        Theme.AddField(Me.cardTechnique, Me.lblTypeDecor, "Type décor", Me.cboTypeDecor, 16, yTech, 304)

        ' grpPhoto (photo card - widened to absorb the leftover width right of the other two cards)
        Me.grpPhoto.Location = New Point(738, 10)
        Me.grpPhoto.Size = New Size(280, 400)
        Theme.ApplyCardStyle(Me.grpPhoto)
        Me.lblCardPhoto.Text = "Photo"
        Me.lblCardPhoto.AutoSize = True
        Me.lblCardPhoto.Location = New Point(16, 14)
        Theme.ApplySectionHeader(Me.lblCardPhoto)
        Me.picPhoto.Location = New Point(16, 44)
        Me.picPhoto.Size = New Size(248, 220)
        Me.picPhoto.BorderStyle = BorderStyle.FixedSingle
        Me.picPhoto.SizeMode = PictureBoxSizeMode.Zoom
        Me.picPhoto.BackColor = Theme.ReadOnlyFill
        Me.btnChargerPhoto.Location = New Point(16, 274)
        Me.btnChargerPhoto.Size = New Size(248, 32)
        Me.btnChargerPhoto.Text = "Choisir photo..."
        Theme.ApplyOutlineButton(Me.btnChargerPhoto)
        Me.btnSupprimerPhoto.Location = New Point(16, 312)
        Me.btnSupprimerPhoto.Size = New Size(248, 32)
        Me.btnSupprimerPhoto.Text = "Supprimer"
        Theme.ApplyMutedButton(Me.btnSupprimerPhoto)
        Me.grpPhoto.Controls.Add(Me.lblCardPhoto)
        Me.grpPhoto.Controls.Add(Me.picPhoto)
        Me.grpPhoto.Controls.Add(Me.btnChargerPhoto)
        Me.grpPhoto.Controls.Add(Me.btnSupprimerPhoto)

        Me.pnlHeaderCards.Controls.Add(Me.cardIdentification)
        Me.pnlHeaderCards.Controls.Add(Me.cardTechnique)
        Me.pnlHeaderCards.Controls.Add(Me.grpPhoto)

        ' grpLignes
        Me.grpLignes.Dock = DockStyle.Fill
        Me.grpLignes.BackColor = Theme.CardBackground
        Me.grpLignes.Padding = New Padding(22, 14, 22, 14)
        Me.grpLignes.Controls.Add(Me.dgvLignes)
        Me.grpLignes.Controls.Add(Me.pnlLignesHeader)

        ' pnlLignesHeader
        Me.pnlLignesHeader.Dock = DockStyle.Top
        Me.pnlLignesHeader.Height = 34
        Me.pnlLignesHeader.Controls.Add(Me.lblLignesTitle)
        Me.pnlLignesHeader.Controls.Add(Me.pnlLignesButtons)

        Me.lblLignesTitle.Dock = DockStyle.Fill
        Me.lblLignesTitle.TextAlign = ContentAlignment.MiddleLeft
        Me.lblLignesTitle.Text = "Lignes de la nomenclature (0)" ' count kept live by RefreshTotals()
        Theme.ApplySectionHeader(Me.lblLignesTitle)

        Me.pnlLignesButtons.Dock = DockStyle.Right
        Me.pnlLignesButtons.AutoSize = True
        Me.pnlLignesButtons.WrapContents = False
        Me.pnlLignesButtons.FlowDirection = FlowDirection.RightToLeft
        Me.btnAjouterLigne.AutoSize = True
        Me.btnAjouterLigne.Height = 28
        Me.btnAjouterLigne.Text = "+ Ajouter ligne"
        Me.btnAjouterLigne.Margin = New Padding(8, 3, 0, 3)
        Theme.ApplyOutlineButton(Me.btnAjouterLigne)
        Me.btnSupprimerLigne.AutoSize = True
        Me.btnSupprimerLigne.Height = 28
        Me.btnSupprimerLigne.Text = "Supprimer ligne"
        Me.btnSupprimerLigne.Margin = New Padding(8, 3, 0, 3)
        Theme.ApplyMutedButton(Me.btnSupprimerLigne)
        Me.pnlLignesButtons.Controls.Add(Me.btnSupprimerLigne)
        Me.pnlLignesButtons.Controls.Add(Me.btnAjouterLigne)

        ' dgvLignes
        Me.dgvLignes.Dock = DockStyle.Fill
        Me.dgvLignes.AllowUserToAddRows = False
        Me.dgvLignes.AllowUserToDeleteRows = False
        Me.dgvLignes.RowHeadersVisible = False
        Me.dgvLignes.MultiSelect = False
        Me.dgvLignes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvLignes.AutoGenerateColumns = False
        Me.dgvLignes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Theme.ApplyGridStyle(Me.dgvLignes)

        Dim colCode As New DataGridViewTextBoxColumn() With {
            .Name = "colCode", .HeaderText = "Code", .DataPropertyName = "Code", .FillWeight = 14
        }
        colCode.MaxInputLength = 20
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
            colCode, colDesignation, colQuantite, colPrix, colDevise, colFabricant, colImprime, colObservation
        })

        ' pnlBottom (footer: total text left, action buttons right - one row, matching FormListe)
        Me.pnlBottom.Dock = DockStyle.Bottom
        Me.pnlBottom.Height = 58
        Me.pnlBottom.BackColor = Theme.CardBackground
        Me.pnlBottom.Padding = New Padding(22, 0, 22, 0)
        Me.pnlBottom.Controls.Add(Me.pnlBottomButtons)
        Me.pnlBottom.Controls.Add(Me.lblTotaux)

        Me.lblTotaux.Dock = DockStyle.Fill
        Me.lblTotaux.TextAlign = ContentAlignment.MiddleLeft
        Me.lblTotaux.ForeColor = Theme.Navy
        Me.lblTotaux.Font = New Font(Theme.BodyFont, FontStyle.Bold)

        ' pnlBottomButtons - right edge flush with pnlBottom's own 22px padding, same as the
        ' table above (grpLignes uses the same 22px padding), so the last button lines up with
        ' the table's right edge exactly.
        Me.pnlBottomButtons.Dock = DockStyle.Right
        Me.pnlBottomButtons.AutoSize = True
        Me.pnlBottomButtons.WrapContents = False
        Me.pnlBottomButtons.FlowDirection = FlowDirection.RightToLeft
        Me.btnEnregistrer.AutoSize = True
        Me.btnEnregistrer.Height = 32
        Me.btnEnregistrer.Text = "Enregistrer"
        Me.btnEnregistrer.Margin = New Padding(8, 0, 0, 0)
        Theme.ApplyPrimaryButton(Me.btnEnregistrer)
        Me.btnAnnuler.AutoSize = True
        Me.btnAnnuler.Height = 32
        Me.btnAnnuler.Text = "Annuler"
        Me.btnAnnuler.Margin = New Padding(8, 0, 0, 0)
        Theme.ApplyDangerButton(Me.btnAnnuler)
        Me.btnApercu.AutoSize = True
        Me.btnApercu.Height = 32
        Me.btnApercu.Text = "Aperçu"
        Me.btnApercu.Margin = New Padding(8, 0, 0, 0)
        Theme.ApplyOutlineButton(Me.btnApercu)
        Me.pnlBottomButtons.Controls.Add(Me.btnEnregistrer)
        Me.pnlBottomButtons.Controls.Add(Me.btnAnnuler)
        Me.pnlBottomButtons.Controls.Add(Me.btnApercu)

        ' FormNomenclature
        ' Note: this form is only ever shown embedded inside FormListe's content panel (never as its
        ' own top-level window), so FormBorderStyle/StartPosition/AcceptButton/CancelButton are
        ' intentionally not set here - FormListe owns the single navy header strip and the window
        ' chrome; embedding overrides FormBorderStyle/TopLevel/Dock at the point of embedding.
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.BackColor = Theme.CardBackground
        Me.ClientSize = New Size(1040, 780)
        Me.Controls.Add(Me.grpLignes)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlHeaderCards)

        CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLignes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cardIdentification.ResumeLayout(False)
        Me.cardIdentification.PerformLayout()
        Me.cardTechnique.ResumeLayout(False)
        Me.cardTechnique.PerformLayout()
        Me.grpPhoto.ResumeLayout(False)
        Me.pnlHeaderCards.ResumeLayout(False)
        Me.pnlLignesButtons.ResumeLayout(False)
        Me.pnlLignesButtons.PerformLayout()
        Me.pnlLignesHeader.ResumeLayout(False)
        Me.grpLignes.ResumeLayout(False)
        Me.pnlBottomButtons.ResumeLayout(False)
        Me.pnlBottomButtons.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents errorProvider As ErrorProvider
    Friend WithEvents pnlHeaderCards As Panel
    Friend WithEvents cardIdentification As Panel
    Friend WithEvents lblCardIdentification As Label
    Friend WithEvents lblCode As Label
    Friend WithEvents txtCode As TextBox
    Friend WithEvents lblNom As Label
    Friend WithEvents txtNom As TextBox
    Friend WithEvents lblDate As Label
    Friend WithEvents dtpDate As DateTimePicker
    Friend WithEvents lblRefCustomer As Label
    Friend WithEvents txtRefCustomer As TextBox
    Friend WithEvents cardTechnique As Panel
    Friend WithEvents lblCardTechnique As Label
    Friend WithEvents lblMarque As Label
    Friend WithEvents txtMarque As TextBox
    Friend WithEvents lblModele As Label
    Friend WithEvents txtModele As TextBox
    Friend WithEvents lblGenCode As Label
    Friend WithEvents txtGenCode As TextBox
    Friend WithEvents lblCouleur As Label
    Friend WithEvents txtCouleur As TextBox
    Friend WithEvents lblNW As Label
    Friend WithEvents txtNW As TextBox
    Friend WithEvents lblGW As Label
    Friend WithEvents txtGW As TextBox
    Friend WithEvents lblFrameSize As Label
    Friend WithEvents cboFrameSize As ComboBox
    Friend WithEvents lblWheelSize As Label
    Friend WithEvents cboWheelSize As ComboBox
    Friend WithEvents lblTypeDecor As Label
    Friend WithEvents cboTypeDecor As ComboBox
    Friend WithEvents grpPhoto As Panel
    Friend WithEvents lblCardPhoto As Label
    Friend WithEvents picPhoto As PictureBox
    Friend WithEvents btnChargerPhoto As Button
    Friend WithEvents btnSupprimerPhoto As Button
    Friend WithEvents grpLignes As Panel
    Friend WithEvents pnlLignesHeader As Panel
    Friend WithEvents lblLignesTitle As Label
    Friend WithEvents pnlLignesButtons As FlowLayoutPanel
    Friend WithEvents btnAjouterLigne As Button
    Friend WithEvents btnSupprimerLigne As Button
    Friend WithEvents dgvLignes As DataGridView
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents lblTotaux As Label
    Friend WithEvents pnlBottomButtons As FlowLayoutPanel
    Friend WithEvents btnEnregistrer As Button
    Friend WithEvents btnAnnuler As Button
    Friend WithEvents btnApercu As Button

End Class
