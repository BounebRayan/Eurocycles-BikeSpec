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

        Me.pnlListView = New Panel()
        Me.pnlTop = New Panel()
        Me.pnlSearchGroup = New FlowLayoutPanel()
        Me.txtSearch = New TextBox()
        Me.btnSearch = New Button()
        Me.btnActualiser = New Button()
        Me.btnNouveau = New Button()
        Me.pnlGridContainer = New Panel()
        Me.dgvNomenclatures = New DataGridView()
        Me.pnlBottom = New Panel()
        Me.lblStatusCount = New Label()
        Me.pnlBottomButtons = New FlowLayoutPanel()
        Me.btnModifier = New Button()
        Me.btnSupprimer = New Button()
        Me.btnApercu = New Button()

        Me.pnlContent = New Panel()

        CType(Me.bsNomenclatures, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvNomenclatures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlListView.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlSearchGroup.SuspendLayout()
        Me.pnlGridContainer.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.pnlBottomButtons.SuspendLayout()
        Me.SuspendLayout()

        ' pnlTop (search row) - search group docked left, Nouveau pinned to the right edge
        Me.pnlTop.Dock = DockStyle.Top
        Me.pnlTop.Height = 64
        Me.pnlTop.BackColor = Theme.CardBackground
        Me.pnlTop.Padding = New Padding(22, 16, 22, 10)
        Me.pnlTop.Controls.Add(Me.btnNouveau)
        Me.pnlTop.Controls.Add(Me.pnlSearchGroup)

        ' pnlSearchGroup
        Me.pnlSearchGroup.Dock = DockStyle.Left
        Me.pnlSearchGroup.AutoSize = True
        Me.pnlSearchGroup.WrapContents = False
        Me.pnlSearchGroup.FlowDirection = FlowDirection.LeftToRight
        Me.pnlSearchGroup.Controls.Add(Me.txtSearch)
        Me.pnlSearchGroup.Controls.Add(Me.btnSearch)
        Me.pnlSearchGroup.Controls.Add(Me.btnActualiser)

        ' txtSearch - Multiline so the explicit Height is honored (WinForms clamps a
        ' single-line TextBox's height to its font, ignoring Height otherwise), so it can
        ' match the buttons' height exactly. KeyDown already suppresses Enter's newline.
        Me.txtSearch.Multiline = True
        Me.txtSearch.Width = 380
        Me.txtSearch.Height = 30
        Me.txtSearch.Font = Theme.BodyFont
        Me.txtSearch.BorderStyle = BorderStyle.FixedSingle
        Me.txtSearch.Margin = New Padding(0, 0, 12, 0)

        ' btnSearch
        Me.btnSearch.AutoSize = True
        Me.btnSearch.Height = 30
        Me.btnSearch.Text = "Rechercher"
        Me.btnSearch.Margin = New Padding(0, 0, 8, 0)
        Theme.ApplyOutlineButton(Me.btnSearch)

        ' btnActualiser (mockup renames this "Réinitialiser")
        Me.btnActualiser.AutoSize = True
        Me.btnActualiser.Height = 30
        Me.btnActualiser.Text = "Réinitialiser"
        Me.btnActualiser.Margin = New Padding(0)
        Theme.ApplyMutedButton(Me.btnActualiser)

        ' btnNouveau - pinned to the right edge of the search row, independent of the input group
        Me.btnNouveau.Dock = DockStyle.Right
        Me.btnNouveau.AutoSize = True
        Me.btnNouveau.Height = 30
        Me.btnNouveau.Text = "+ Nouveau"
        Theme.ApplyPrimaryButton(Me.btnNouveau)

        ' pnlGridContainer - same 22px x-padding as the search row, so the grid's left edge
        ' lines up with the search input's left edge.
        Me.pnlGridContainer.Dock = DockStyle.Fill
        Me.pnlGridContainer.BackColor = Theme.CardBackground
        Me.pnlGridContainer.Padding = New Padding(22, 0, 22, 16)
        Me.pnlGridContainer.Controls.Add(Me.dgvNomenclatures)

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
        Theme.ApplyGridStyle(Me.dgvNomenclatures)

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

        ' pnlBottom (footer: status text left, action buttons right - one row, per the mockup)
        Me.pnlBottom.Dock = DockStyle.Bottom
        Me.pnlBottom.Height = 58
        Me.pnlBottom.BackColor = Theme.CardBackground
        Me.pnlBottom.Padding = New Padding(22, 0, 22, 0)
        Me.pnlBottom.Controls.Add(Me.pnlBottomButtons)
        Me.pnlBottom.Controls.Add(Me.lblStatusCount)

        ' lblStatusCount - navy, to match the app's branding (not the mockup's green token)
        Me.lblStatusCount.Dock = DockStyle.Fill
        Me.lblStatusCount.TextAlign = ContentAlignment.MiddleLeft
        Me.lblStatusCount.ForeColor = Theme.Navy
        Me.lblStatusCount.Font = New Font(Theme.BodyFont, FontStyle.Bold)
        Me.lblStatusCount.Text = String.Empty

        ' pnlBottomButtons
        Me.pnlBottomButtons.Dock = DockStyle.Right
        Me.pnlBottomButtons.AutoSize = True
        Me.pnlBottomButtons.WrapContents = False
        Me.pnlBottomButtons.FlowDirection = FlowDirection.RightToLeft
        Me.pnlBottomButtons.Padding = New Padding(0, 12, 0, 12)
        Me.btnModifier.AutoSize = True
        Me.btnModifier.Height = 30
        Me.btnModifier.Text = "Modifier"
        Me.btnModifier.Enabled = False
        Me.btnModifier.Margin = New Padding(8, 0, 0, 0)
        Theme.ApplyOutlineButton(Me.btnModifier)

        Me.btnSupprimer.AutoSize = True
        Me.btnSupprimer.Height = 30
        Me.btnSupprimer.Text = "Supprimer"
        Me.btnSupprimer.Enabled = False
        Me.btnSupprimer.Margin = New Padding(8, 0, 0, 0)
        Theme.ApplyDangerButton(Me.btnSupprimer)

        Me.btnApercu.AutoSize = True
        Me.btnApercu.Height = 30
        Me.btnApercu.Text = "Aperçu"
        Me.btnApercu.Enabled = False
        Me.btnApercu.Margin = New Padding(8, 0, 0, 0)
        Theme.ApplyPrimaryButton(Me.btnApercu)

        ' Order controls RightToLeft-flow so the visual order reads Modifier, Supprimer, Aperçu
        ' (rightmost = Aperçu, matching the mockup) - first added ends up rightmost.
        Me.pnlBottomButtons.Controls.Add(Me.btnApercu)
        Me.pnlBottomButtons.Controls.Add(Me.btnSupprimer)
        Me.pnlBottomButtons.Controls.Add(Me.btnModifier)

        ' pnlListView - everything above, as one swappable unit
        Me.pnlListView.Dock = DockStyle.Fill
        Me.pnlListView.Visible = True
        Me.pnlListView.Controls.Add(Me.pnlGridContainer)
        Me.pnlListView.Controls.Add(Me.pnlBottom)
        Me.pnlListView.Controls.Add(Me.pnlTop)

        ' pnlContent - hosts an embedded FormNomenclature/FormApercu in place of the list view.
        ' Hidden until Form1.vb's navigation code embeds something into it.
        Me.pnlContent.Dock = DockStyle.Fill
        Me.pnlContent.Visible = False
        Me.pnlContent.BackColor = Theme.CardBackground

        ' Form1
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.BackColor = Theme.CardBackground
        Me.ClientSize = New Size(1040, 700)
        Me.Text = "Eurocycles BikeSpec" ' the OS window/taskbar title stays constant regardless of screen
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlListView)
        Me.Controls.Add(Theme.BuildHeaderStrip("BikeSpec » Nomenclatures", Me.lblHeaderSubtitle))

        CType(Me.bsNomenclatures, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvNomenclatures, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchGroup.ResumeLayout(False)
        Me.pnlSearchGroup.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlGridContainer.ResumeLayout(False)
        Me.pnlBottomButtons.ResumeLayout(False)
        Me.pnlBottomButtons.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlListView.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents bsNomenclatures As BindingSource
    Friend WithEvents lblHeaderSubtitle As Label
    Friend WithEvents pnlListView As Panel
    Friend WithEvents pnlTop As Panel
    Friend WithEvents pnlSearchGroup As FlowLayoutPanel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnNouveau As Button
    Friend WithEvents btnActualiser As Button
    Friend WithEvents pnlGridContainer As Panel
    Friend WithEvents dgvNomenclatures As DataGridView
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents pnlBottomButtons As FlowLayoutPanel
    Friend WithEvents btnModifier As Button
    Friend WithEvents btnSupprimer As Button
    Friend WithEvents btnApercu As Button
    Friend WithEvents lblStatusCount As Label
    Friend WithEvents pnlContent As Panel

End Class
