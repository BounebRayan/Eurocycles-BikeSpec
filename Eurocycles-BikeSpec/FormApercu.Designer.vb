<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormApercu
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

        Me.pnlHeaderCards = New Panel()
        Me.cardIdentification = New Panel()
        Me.lblCardIdentification = New Label()
        Me.lblCode = New Label()
        Me.txtCode = New TextBox()
        Me.lblNom = New Label()
        Me.txtNom = New TextBox()
        Me.lblDate = New Label()
        Me.txtDate = New TextBox()
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
        Me.txtFrameSize = New TextBox()
        Me.lblWheelSize = New Label()
        Me.txtWheelSize = New TextBox()
        Me.lblTypeDecor = New Label()
        Me.txtTypeDecor = New TextBox()

        Me.grpPhoto = New Panel()
        Me.lblCardPhoto = New Label()
        Me.picPhoto = New PictureBox()

        Me.grpLignes = New Panel()
        Me.lblLignesTitle = New Label()
        Me.dgvLignes = New DataGridView()
        Me.pnlTotaux = New Panel()
        Me.lblTotaux = New Label()

        Me.pnlBottom = New FlowLayoutPanel()
        Me.btnImprimer = New Button()
        Me.btnFermer = New Button()
        Me.btnModifier = New Button()

        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLignes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeaderCards.SuspendLayout()
        Me.cardIdentification.SuspendLayout()
        Me.cardTechnique.SuspendLayout()
        Me.grpPhoto.SuspendLayout()
        Me.grpLignes.SuspendLayout()
        Me.pnlTotaux.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()

        ' pnlHeaderCards
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

        SetupReadOnlyField(Me.txtCode)
        SetupReadOnlyField(Me.txtNom)
        SetupReadOnlyField(Me.txtDate)
        SetupReadOnlyField(Me.txtRefCustomer)

        Dim yId = 44
        yId = Theme.AddField(Me.cardIdentification, Me.lblCode, "Code", Me.txtCode, 16, yId, 304)
        yId = Theme.AddField(Me.cardIdentification, Me.lblNom, "Nom", Me.txtNom, 16, yId, 304)
        yId = Theme.AddField(Me.cardIdentification, Me.lblDate, "Date", Me.txtDate, 16, yId, 304)
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

        SetupReadOnlyField(Me.txtMarque)
        SetupReadOnlyField(Me.txtModele)
        SetupReadOnlyField(Me.txtGenCode)
        SetupReadOnlyField(Me.txtCouleur)
        SetupReadOnlyField(Me.txtNW)
        SetupReadOnlyField(Me.txtGW)
        SetupReadOnlyField(Me.txtFrameSize)
        SetupReadOnlyField(Me.txtWheelSize)
        SetupReadOnlyField(Me.txtTypeDecor)

        Dim yTech = 44
        Theme.AddField(Me.cardTechnique, Me.lblMarque, "Marque", Me.txtMarque, 16, yTech, 142)
        yTech = Theme.AddField(Me.cardTechnique, Me.lblModele, "Modèle", Me.txtModele, 178, yTech, 142)
        Theme.AddField(Me.cardTechnique, Me.lblGenCode, "GenCode", Me.txtGenCode, 16, yTech, 142)
        yTech = Theme.AddField(Me.cardTechnique, Me.lblCouleur, "Couleur", Me.txtCouleur, 178, yTech, 142)
        Theme.AddField(Me.cardTechnique, Me.lblNW, "NW (kg)", Me.txtNW, 16, yTech, 142)
        yTech = Theme.AddField(Me.cardTechnique, Me.lblGW, "GW (kg)", Me.txtGW, 178, yTech, 142)
        Theme.AddField(Me.cardTechnique, Me.lblFrameSize, "Taille cadre", Me.txtFrameSize, 16, yTech, 142)
        yTech = Theme.AddField(Me.cardTechnique, Me.lblWheelSize, "Taille roue", Me.txtWheelSize, 178, yTech, 142)
        Theme.AddField(Me.cardTechnique, Me.lblTypeDecor, "Type décor", Me.txtTypeDecor, 16, yTech, 304)

        ' grpPhoto (photo card)
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
        Me.grpPhoto.Controls.Add(Me.lblCardPhoto)
        Me.grpPhoto.Controls.Add(Me.picPhoto)

        Me.pnlHeaderCards.Controls.Add(Me.cardIdentification)
        Me.pnlHeaderCards.Controls.Add(Me.cardTechnique)
        Me.pnlHeaderCards.Controls.Add(Me.grpPhoto)

        ' grpLignes
        Me.grpLignes.Dock = DockStyle.Fill
        Me.grpLignes.BackColor = Theme.CardBackground
        Me.grpLignes.Padding = New Padding(22, 14, 22, 14)
        Me.grpLignes.Controls.Add(Me.dgvLignes)
        Me.grpLignes.Controls.Add(Me.pnlTotaux)
        Me.grpLignes.Controls.Add(Me.lblLignesTitle)

        Me.lblLignesTitle.Dock = DockStyle.Top
        Me.lblLignesTitle.Height = 34
        Me.lblLignesTitle.TextAlign = ContentAlignment.MiddleLeft
        Me.lblLignesTitle.Text = "Lignes de la nomenclature"
        Theme.ApplySectionHeader(Me.lblLignesTitle)

        ' dgvLignes
        Me.dgvLignes.Dock = DockStyle.Fill
        Me.dgvLignes.ReadOnly = True
        Me.dgvLignes.AllowUserToAddRows = False
        Me.dgvLignes.AllowUserToDeleteRows = False
        Me.dgvLignes.AllowUserToResizeRows = False
        Me.dgvLignes.RowHeadersVisible = False
        Me.dgvLignes.MultiSelect = False
        Me.dgvLignes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvLignes.AutoGenerateColumns = False
        Me.dgvLignes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Theme.ApplyGridStyle(Me.dgvLignes)

        Dim colDesignation As New DataGridViewTextBoxColumn() With {
            .Name = "colDesignation", .HeaderText = "Désignation", .DataPropertyName = "Designation", .FillWeight = 26
        }
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
        Dim colImprime As New DataGridViewCheckBoxColumn() With {
            .Name = "colImprime", .HeaderText = "Imprimé", .DataPropertyName = "Imprime", .FillWeight = 8
        }
        Dim colDevise As New DataGridViewTextBoxColumn() With {
            .Name = "colDevise", .HeaderText = "Devise", .DataPropertyName = "Devise", .FillWeight = 8
        }
        Dim colObservation As New DataGridViewTextBoxColumn() With {
            .Name = "colObservation", .HeaderText = "Observation", .DataPropertyName = "Observation", .FillWeight = 22
        }

        Me.dgvLignes.Columns.AddRange(New DataGridViewColumn() {
            colDesignation, colQuantite, colPrix, colFabricant, colImprime, colDevise, colObservation
        })

        ' pnlTotaux
        Me.pnlTotaux.Dock = DockStyle.Bottom
        Me.pnlTotaux.Height = 32
        Me.lblTotaux.Dock = DockStyle.Fill
        Me.lblTotaux.TextAlign = ContentAlignment.MiddleLeft
        Me.lblTotaux.ForeColor = Theme.Navy
        Me.lblTotaux.Font = New Font(Theme.BodyFont, FontStyle.Bold)
        Me.pnlTotaux.Controls.Add(Me.lblTotaux)

        ' pnlBottom
        Me.pnlBottom.Dock = DockStyle.Bottom
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.WrapContents = False
        Me.pnlBottom.FlowDirection = FlowDirection.RightToLeft
        Me.pnlBottom.BackColor = Theme.CardBackground
        Me.pnlBottom.Padding = New Padding(22, 12, 22, 12)
        Me.btnImprimer.AutoSize = True
        Me.btnImprimer.Height = 32
        Me.btnImprimer.Text = "Imprimer"
        Me.btnImprimer.Margin = New Padding(8, 0, 0, 0)
        Theme.ApplyPrimaryButton(Me.btnImprimer)
        Me.btnFermer.AutoSize = True
        Me.btnFermer.Height = 32
        Me.btnFermer.Text = "Fermer"
        Me.btnFermer.Margin = New Padding(8, 0, 0, 0)
        Theme.ApplyMutedButton(Me.btnFermer)
        Me.btnModifier.AutoSize = True
        Me.btnModifier.Height = 32
        Me.btnModifier.Text = "Modifier"
        Me.btnModifier.Margin = New Padding(8, 0, 0, 0)
        Theme.ApplyOutlineButton(Me.btnModifier)
        Me.pnlBottom.Controls.Add(Me.btnImprimer)
        Me.pnlBottom.Controls.Add(Me.btnFermer)
        Me.pnlBottom.Controls.Add(Me.btnModifier)

        ' FormApercu
        ' Note: only ever shown embedded inside Form1's content panel - see FormNomenclature's
        ' equivalent comment.
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.BackColor = Theme.CardBackground
        Me.ClientSize = New Size(1040, 780)
        Me.Controls.Add(Me.grpLignes)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlHeaderCards)

        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLignes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cardIdentification.ResumeLayout(False)
        Me.cardIdentification.PerformLayout()
        Me.cardTechnique.ResumeLayout(False)
        Me.cardTechnique.PerformLayout()
        Me.grpPhoto.ResumeLayout(False)
        Me.pnlHeaderCards.ResumeLayout(False)
        Me.pnlTotaux.ResumeLayout(False)
        Me.grpLignes.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    ''' <summary>Applies the design handoff's read-only field look on top of Theme.ApplyReadOnlyField
    ''' (extracted since AddField below re-sets Font/BorderStyle after this runs; order matters,
    ''' so the read-only look is applied to every field up front, before positioning).</summary>
    Private Shared Sub SetupReadOnlyField(textBox As TextBox)
        Theme.ApplyReadOnlyField(textBox)
    End Sub

    Friend WithEvents pnlHeaderCards As Panel
    Friend WithEvents cardIdentification As Panel
    Friend WithEvents lblCardIdentification As Label
    Friend WithEvents lblCode As Label
    Friend WithEvents txtCode As TextBox
    Friend WithEvents lblNom As Label
    Friend WithEvents txtNom As TextBox
    Friend WithEvents lblDate As Label
    Friend WithEvents txtDate As TextBox
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
    Friend WithEvents txtFrameSize As TextBox
    Friend WithEvents lblWheelSize As Label
    Friend WithEvents txtWheelSize As TextBox
    Friend WithEvents lblTypeDecor As Label
    Friend WithEvents txtTypeDecor As TextBox
    Friend WithEvents grpPhoto As Panel
    Friend WithEvents lblCardPhoto As Label
    Friend WithEvents picPhoto As PictureBox
    Friend WithEvents grpLignes As Panel
    Friend WithEvents lblLignesTitle As Label
    Friend WithEvents dgvLignes As DataGridView
    Friend WithEvents pnlTotaux As Panel
    Friend WithEvents lblTotaux As Label
    Friend WithEvents pnlBottom As FlowLayoutPanel
    Friend WithEvents btnImprimer As Button
    Friend WithEvents btnFermer As Button
    Friend WithEvents btnModifier As Button

End Class
