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

        Me.pnlHeader = New Panel()
        Me.tlpHeader = New TableLayoutPanel()
        Me.lblCode = New Label()
        Me.txtCode = New TextBox()
        Me.lblNom = New Label()
        Me.txtNom = New TextBox()
        Me.lblDate = New Label()
        Me.txtDate = New TextBox()
        Me.lblMarque = New Label()
        Me.txtMarque = New TextBox()
        Me.lblModele = New Label()
        Me.txtModele = New TextBox()
        Me.lblGenCode = New Label()
        Me.txtGenCode = New TextBox()
        Me.lblNW = New Label()
        Me.txtNW = New TextBox()
        Me.lblGW = New Label()
        Me.txtGW = New TextBox()
        Me.lblFrameSize = New Label()
        Me.txtFrameSize = New TextBox()
        Me.lblWheelSize = New Label()
        Me.txtWheelSize = New TextBox()
        Me.lblRefCustomer = New Label()
        Me.txtRefCustomer = New TextBox()
        Me.lblCouleur = New Label()
        Me.txtCouleur = New TextBox()
        Me.lblTypeDecor = New Label()
        Me.txtTypeDecor = New TextBox()

        Me.grpPhoto = New GroupBox()
        Me.picPhoto = New PictureBox()

        Me.grpLignes = New GroupBox()
        Me.dgvLignes = New DataGridView()
        Me.pnlTotaux = New Panel()
        Me.lblTotaux = New Label()

        Me.pnlBottom = New FlowLayoutPanel()
        Me.btnImprimer = New Button()
        Me.btnFermer = New Button()

        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLignes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.tlpHeader.SuspendLayout()
        Me.grpPhoto.SuspendLayout()
        Me.grpLignes.SuspendLayout()
        Me.pnlTotaux.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()

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

        SetupReadOnlyField(Me.lblCode, "Code", Me.txtCode)
        SetupReadOnlyField(Me.lblNom, "Nom", Me.txtNom)
        SetupReadOnlyField(Me.lblDate, "Date", Me.txtDate)
        SetupReadOnlyField(Me.lblMarque, "Marque", Me.txtMarque)
        SetupReadOnlyField(Me.lblModele, "Modèle", Me.txtModele)
        SetupReadOnlyField(Me.lblGenCode, "GenCode", Me.txtGenCode)
        SetupReadOnlyField(Me.lblNW, "NW (kg)", Me.txtNW)
        SetupReadOnlyField(Me.lblGW, "GW (kg)", Me.txtGW)
        SetupReadOnlyField(Me.lblFrameSize, "Taille cadre", Me.txtFrameSize)
        SetupReadOnlyField(Me.lblWheelSize, "Taille roue", Me.txtWheelSize)
        SetupReadOnlyField(Me.lblRefCustomer, "Réf. client", Me.txtRefCustomer)
        SetupReadOnlyField(Me.lblCouleur, "Couleur", Me.txtCouleur)
        SetupReadOnlyField(Me.lblTypeDecor, "Type décor", Me.txtTypeDecor)

        Me.tlpHeader.Controls.Add(Me.lblCode, 0, 0)
        Me.tlpHeader.Controls.Add(Me.txtCode, 1, 0)
        Me.tlpHeader.Controls.Add(Me.lblNom, 2, 0)
        Me.tlpHeader.Controls.Add(Me.txtNom, 3, 0)
        Me.tlpHeader.Controls.Add(Me.lblDate, 0, 1)
        Me.tlpHeader.Controls.Add(Me.txtDate, 1, 1)
        Me.tlpHeader.Controls.Add(Me.lblMarque, 2, 1)
        Me.tlpHeader.Controls.Add(Me.txtMarque, 3, 1)
        Me.tlpHeader.Controls.Add(Me.lblModele, 0, 2)
        Me.tlpHeader.Controls.Add(Me.txtModele, 1, 2)
        Me.tlpHeader.Controls.Add(Me.lblGenCode, 2, 2)
        Me.tlpHeader.Controls.Add(Me.txtGenCode, 3, 2)
        Me.tlpHeader.Controls.Add(Me.lblNW, 0, 3)
        Me.tlpHeader.Controls.Add(Me.txtNW, 1, 3)
        Me.tlpHeader.Controls.Add(Me.lblGW, 2, 3)
        Me.tlpHeader.Controls.Add(Me.txtGW, 3, 3)
        Me.tlpHeader.Controls.Add(Me.lblFrameSize, 0, 4)
        Me.tlpHeader.Controls.Add(Me.txtFrameSize, 1, 4)
        Me.tlpHeader.Controls.Add(Me.lblWheelSize, 2, 4)
        Me.tlpHeader.Controls.Add(Me.txtWheelSize, 3, 4)
        Me.tlpHeader.Controls.Add(Me.lblRefCustomer, 0, 5)
        Me.tlpHeader.Controls.Add(Me.txtRefCustomer, 1, 5)
        Me.tlpHeader.Controls.Add(Me.lblCouleur, 2, 5)
        Me.tlpHeader.Controls.Add(Me.txtCouleur, 3, 5)
        Me.tlpHeader.Controls.Add(Me.lblTypeDecor, 0, 6)
        Me.tlpHeader.Controls.Add(Me.txtTypeDecor, 1, 6)

        ' grpPhoto
        Me.grpPhoto.Dock = DockStyle.Right
        Me.grpPhoto.Width = 200
        Me.grpPhoto.Text = "Photo"
        Me.picPhoto.Location = New Point(15, 25)
        Me.picPhoto.Size = New Size(160, 180)
        Me.picPhoto.BorderStyle = BorderStyle.FixedSingle
        Me.picPhoto.SizeMode = PictureBoxSizeMode.Zoom
        Me.grpPhoto.Controls.Add(Me.picPhoto)

        ' grpLignes
        Me.grpLignes.Dock = DockStyle.Fill
        Me.grpLignes.Text = "Lignes de nomenclature"
        Me.grpLignes.Controls.Add(Me.dgvLignes)
        Me.grpLignes.Controls.Add(Me.pnlTotaux)

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
        Me.lblTotaux.TextAlign = ContentAlignment.MiddleRight
        Me.lblTotaux.Font = New Font(Me.lblTotaux.Font, FontStyle.Bold)
        Me.lblTotaux.Padding = New Padding(0, 0, 10, 0)
        Me.pnlTotaux.Controls.Add(Me.lblTotaux)

        ' pnlBottom
        Me.pnlBottom.Dock = DockStyle.Bottom
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.FlowDirection = FlowDirection.RightToLeft
        Me.pnlBottom.Padding = New Padding(10)
        Me.btnImprimer.AutoSize = True
        Me.btnImprimer.Text = "Imprimer"
        Me.btnImprimer.Margin = New Padding(3)
        Me.btnFermer.AutoSize = True
        Me.btnFermer.Text = "Fermer"
        Me.btnFermer.Margin = New Padding(3)
        Me.pnlBottom.Controls.Add(Me.btnFermer)
        Me.pnlBottom.Controls.Add(Me.btnImprimer)

        ' FormApercu
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(1000, 700)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.CancelButton = Me.btnFermer
        Me.Text = "Aperçu"
        Me.Controls.Add(Me.grpLignes)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlHeader)

        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLignes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.tlpHeader.ResumeLayout(False)
        Me.grpPhoto.ResumeLayout(False)
        Me.grpLignes.ResumeLayout(False)
        Me.pnlTotaux.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    ''' <summary>Configures a label + read-only value TextBox pair used throughout the header block.</summary>
    Private Shared Sub SetupReadOnlyField(label As Label, text As String, textBox As TextBox)
        label.Text = text
        label.Dock = DockStyle.Fill
        label.TextAlign = ContentAlignment.MiddleLeft
        textBox.Dock = DockStyle.Fill
        textBox.ReadOnly = True
        textBox.TabStop = False
        textBox.BackColor = SystemColors.Control
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents tlpHeader As TableLayoutPanel
    Friend WithEvents lblCode As Label
    Friend WithEvents txtCode As TextBox
    Friend WithEvents lblNom As Label
    Friend WithEvents txtNom As TextBox
    Friend WithEvents lblDate As Label
    Friend WithEvents txtDate As TextBox
    Friend WithEvents lblMarque As Label
    Friend WithEvents txtMarque As TextBox
    Friend WithEvents lblModele As Label
    Friend WithEvents txtModele As TextBox
    Friend WithEvents lblGenCode As Label
    Friend WithEvents txtGenCode As TextBox
    Friend WithEvents lblNW As Label
    Friend WithEvents txtNW As TextBox
    Friend WithEvents lblGW As Label
    Friend WithEvents txtGW As TextBox
    Friend WithEvents lblFrameSize As Label
    Friend WithEvents txtFrameSize As TextBox
    Friend WithEvents lblWheelSize As Label
    Friend WithEvents txtWheelSize As TextBox
    Friend WithEvents lblRefCustomer As Label
    Friend WithEvents txtRefCustomer As TextBox
    Friend WithEvents lblCouleur As Label
    Friend WithEvents txtCouleur As TextBox
    Friend WithEvents lblTypeDecor As Label
    Friend WithEvents txtTypeDecor As TextBox
    Friend WithEvents grpPhoto As GroupBox
    Friend WithEvents picPhoto As PictureBox
    Friend WithEvents grpLignes As GroupBox
    Friend WithEvents dgvLignes As DataGridView
    Friend WithEvents pnlTotaux As Panel
    Friend WithEvents lblTotaux As Label
    Friend WithEvents pnlBottom As FlowLayoutPanel
    Friend WithEvents btnImprimer As Button
    Friend WithEvents btnFermer As Button

End Class
