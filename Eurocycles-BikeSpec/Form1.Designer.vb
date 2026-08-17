<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()

        Dim colCode As New DataGridViewTextBoxColumn()
        Dim colNom As New DataGridViewTextBoxColumn()
        Dim colDate As New DataGridViewTextBoxColumn()
        Dim colMarque As New DataGridViewTextBoxColumn()
        Dim colModele As New DataGridViewTextBoxColumn()
        Dim colFrameSize As New DataGridViewTextBoxColumn()
        Dim colWheelSize As New DataGridViewTextBoxColumn()
        Dim colCouleur As New DataGridViewTextBoxColumn()

        Me.bsNomenclatures = New BindingSource(Me.components)
        Me.pnlTop = New FlowLayoutPanel()
        Me.lblSearch = New Label()
        Me.txtSearch = New TextBox()
        Me.btnSearch = New Button()
        Me.btnNouveau = New Button()
        Me.btnActualiser = New Button()
        Me.dgvNomenclatures = New DataGridView()
        Me.pnlBottom = New FlowLayoutPanel()
        Me.btnModifier = New Button()
        Me.btnSupprimer = New Button()
        Me.btnApercu = New Button()

        CType(Me.bsNomenclatures, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvNomenclatures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()

        ' pnlTop
        Me.pnlTop.Dock = DockStyle.Top
        Me.pnlTop.AutoSize = True
        Me.pnlTop.FlowDirection = FlowDirection.LeftToRight
        Me.pnlTop.WrapContents = False
        Me.pnlTop.Padding = New Padding(8)
        Me.pnlTop.Controls.Add(Me.lblSearch)
        Me.pnlTop.Controls.Add(Me.txtSearch)
        Me.pnlTop.Controls.Add(Me.btnSearch)
        Me.pnlTop.Controls.Add(Me.btnNouveau)
        Me.pnlTop.Controls.Add(Me.btnActualiser)

        ' lblSearch
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Text = "Rechercher :"
        Me.lblSearch.Margin = New Padding(3, 8, 3, 3)

        ' txtSearch
        Me.txtSearch.Width = 220
        Me.txtSearch.Margin = New Padding(3, 4, 12, 3)

        ' btnSearch
        Me.btnSearch.AutoSize = True
        Me.btnSearch.Text = "Rechercher"
        Me.btnSearch.Margin = New Padding(3, 3, 20, 3)

        ' btnNouveau
        Me.btnNouveau.AutoSize = True
        Me.btnNouveau.Text = "Nouveau"
        Me.btnNouveau.Margin = New Padding(3)

        ' btnActualiser
        Me.btnActualiser.AutoSize = True
        Me.btnActualiser.Text = "Actualiser"
        Me.btnActualiser.Margin = New Padding(3)

        ' dgvNomenclatures
        Me.dgvNomenclatures.Dock = DockStyle.Fill
        Me.dgvNomenclatures.ReadOnly = True
        Me.dgvNomenclatures.AllowUserToAddRows = False
        Me.dgvNomenclatures.AllowUserToDeleteRows = False
        Me.dgvNomenclatures.AllowUserToResizeRows = False
        Me.dgvNomenclatures.RowHeadersVisible = False
        Me.dgvNomenclatures.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvNomenclatures.MultiSelect = False
        Me.dgvNomenclatures.AutoGenerateColumns = False
        Me.dgvNomenclatures.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        colCode.Name = "colCode"
        colCode.HeaderText = "Code"
        colCode.DataPropertyName = "Code"

        colNom.Name = "colNom"
        colNom.HeaderText = "Nom"
        colNom.DataPropertyName = "Nom"

        colDate.Name = "colDate"
        colDate.HeaderText = "Date"
        colDate.DataPropertyName = "Date"
        colDate.DefaultCellStyle.Format = "dd/MM/yyyy"

        colMarque.Name = "colMarque"
        colMarque.HeaderText = "Marque"
        colMarque.DataPropertyName = "Marque"

        colModele.Name = "colModele"
        colModele.HeaderText = "Modèle"
        colModele.DataPropertyName = "Modele"

        colFrameSize.Name = "colFrameSize"
        colFrameSize.HeaderText = "Taille cadre"
        colFrameSize.DataPropertyName = "FrameSize"

        colWheelSize.Name = "colWheelSize"
        colWheelSize.HeaderText = "Taille roue"
        colWheelSize.DataPropertyName = "WheelSize"

        colCouleur.Name = "colCouleur"
        colCouleur.HeaderText = "Couleur"
        colCouleur.DataPropertyName = "Couleur"

        Me.dgvNomenclatures.Columns.AddRange(New DataGridViewColumn() {
            colCode, colNom, colDate, colMarque, colModele, colFrameSize, colWheelSize, colCouleur
        })

        ' pnlBottom
        Me.pnlBottom.Dock = DockStyle.Bottom
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.FlowDirection = FlowDirection.LeftToRight
        Me.pnlBottom.WrapContents = False
        Me.pnlBottom.Padding = New Padding(8)
        Me.pnlBottom.Controls.Add(Me.btnModifier)
        Me.pnlBottom.Controls.Add(Me.btnSupprimer)
        Me.pnlBottom.Controls.Add(Me.btnApercu)

        ' btnModifier
        Me.btnModifier.AutoSize = True
        Me.btnModifier.Text = "Modifier"
        Me.btnModifier.Enabled = False
        Me.btnModifier.Margin = New Padding(3)

        ' btnSupprimer
        Me.btnSupprimer.AutoSize = True
        Me.btnSupprimer.Text = "Supprimer"
        Me.btnSupprimer.Enabled = False
        Me.btnSupprimer.Margin = New Padding(3)

        ' btnApercu
        Me.btnApercu.AutoSize = True
        Me.btnApercu.Text = "Aperçu"
        Me.btnApercu.Enabled = False
        Me.btnApercu.Margin = New Padding(3)

        ' Form1
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(1000, 600)
        Me.Text = "Eurocycles BikeSpec - Nomenclatures"
        Me.Controls.Add(Me.dgvNomenclatures)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)

        CType(Me.bsNomenclatures, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvNomenclatures, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents bsNomenclatures As BindingSource
    Friend WithEvents pnlTop As FlowLayoutPanel
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnNouveau As Button
    Friend WithEvents btnActualiser As Button
    Friend WithEvents dgvNomenclatures As DataGridView
    Friend WithEvents pnlBottom As FlowLayoutPanel
    Friend WithEvents btnModifier As Button
    Friend WithEvents btnSupprimer As Button
    Friend WithEvents btnApercu As Button

End Class
