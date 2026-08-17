Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.InteropServices

''' <summary>
''' Central color/typography palette matching the Eurocycles brand design
''' handoff (design_handoff_bikespec_ui/README.md). Keeps styling consistent
''' across Form1, FormNomenclature, and FormApercu without duplicating hex
''' values or re-deriving the same control styling in three places.
''' </summary>
Public Module Theme

    Public ReadOnly Navy As Color = ColorTranslator.FromHtml("#14213D")
    Public ReadOnly NavyHover As Color = ColorTranslator.FromHtml("#1F2E52")
    Public ReadOnly HeaderSubtitle As Color = ColorTranslator.FromHtml("#C9D2E3")
    Public ReadOnly Yellow As Color = ColorTranslator.FromHtml("#F5D300")
    Public ReadOnly YellowHover As Color = ColorTranslator.FromHtml("#E0C000")
    Public ReadOnly Danger As Color = ColorTranslator.FromHtml("#C0392B")
    Public ReadOnly Success As Color = ColorTranslator.FromHtml("#2E7D46")
    Public ReadOnly CardBackground As Color = Color.White
    Public ReadOnly BorderColor As Color = ColorTranslator.FromHtml("#E1E4EA")
    Public ReadOnly InputBorder As Color = ColorTranslator.FromHtml("#CBD2DE")
    Public ReadOnly MutedText As Color = ColorTranslator.FromHtml("#6B7686")
    Public ReadOnly PlaceholderText As Color = ColorTranslator.FromHtml("#9AA4B2")
    Public ReadOnly ReadOnlyFill As Color = ColorTranslator.FromHtml("#F4F5F7")
    Public ReadOnly HoverFill As Color = ColorTranslator.FromHtml("#F5F6F8")

    Public ReadOnly SectionHeaderFont As New Font("Segoe UI", 9.0F, FontStyle.Bold)
    Public ReadOnly LabelFont As New Font("Segoe UI", 8.5F, FontStyle.Regular)
    Public ReadOnly BodyFont As New Font("Segoe UI", 9.5F, FontStyle.Regular)
    Public ReadOnly ButtonFont As New Font("Segoe UI", 9.0F, FontStyle.Bold)
    Public ReadOnly HeaderGridFont As New Font("Segoe UI", 9.0F, FontStyle.Bold)

    ''' <summary>Filled brand-yellow "primary action" button (Enregistrer, Nouveau, Aperçu, Imprimer).</summary>
    Public Sub ApplyPrimaryButton(button As Button)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.FlatAppearance.MouseOverBackColor = YellowHover
        button.FlatAppearance.MouseDownBackColor = YellowHover
        button.BackColor = Yellow
        button.ForeColor = Navy
        button.Font = ButtonFont
        button.Cursor = Cursors.Hand
        button.UseVisualStyleBackColor = False
    End Sub

    ''' <summary>Outlined button with a given border/text color (navy by default).</summary>
    Public Sub ApplyOutlineButton(button As Button, Optional accent As Color? = Nothing)
        Dim color = If(accent, Navy)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 1
        button.FlatAppearance.BorderColor = color
        button.FlatAppearance.MouseOverBackColor = HoverFill
        button.BackColor = CardBackground
        button.ForeColor = color
        button.Font = ButtonFont
        button.Cursor = Cursors.Hand
        button.UseVisualStyleBackColor = False
    End Sub

    ''' <summary>Outlined muted/gray button (Annuler, Réinitialiser, Fermer, Supprimer ligne).</summary>
    Public Sub ApplyMutedButton(button As Button)
        ApplyOutlineButton(button, InputBorder)
        button.ForeColor = MutedText
    End Sub

    ''' <summary>Outlined danger/red button (Supprimer).</summary>
    Public Sub ApplyDangerButton(button As Button)
        ApplyOutlineButton(button, Danger)
    End Sub

    ''' <summary>Navy header row, white bold header text, navy/white inverted selected-row highlight.</summary>
    Public Sub ApplyGridStyle(grid As DataGridView)
        grid.BackgroundColor = CardBackground
        grid.BorderStyle = BorderStyle.FixedSingle
        grid.GridColor = BorderColor
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersDefaultCellStyle.BackColor = Navy
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Navy
        grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White
        grid.ColumnHeadersDefaultCellStyle.Font = HeaderGridFont
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        grid.ColumnHeadersHeight = 34
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        grid.DefaultCellStyle.BackColor = CardBackground
        grid.DefaultCellStyle.ForeColor = Navy
        grid.DefaultCellStyle.Font = BodyFont
        grid.DefaultCellStyle.SelectionBackColor = Navy
        grid.DefaultCellStyle.SelectionForeColor = Color.White
        grid.DefaultCellStyle.Padding = New Padding(4, 2, 4, 2)
        grid.AlternatingRowsDefaultCellStyle.BackColor = CardBackground
        grid.RowTemplate.Height = 30
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
    End Sub

    ''' <summary>Field caption label (small, muted gray).</summary>
    Public Sub ApplyFieldLabel(label As Label)
        label.Font = LabelFont
        label.ForeColor = MutedText
    End Sub

    ''' <summary>Uppercase card/section header label (e.g. "IDENTIFICATION").</summary>
    Public Sub ApplySectionHeader(label As Label)
        label.Font = SectionHeaderFont
        label.ForeColor = Navy
        label.Text = label.Text.ToUpperInvariant()
    End Sub

    ''' <summary>Card container: white background, thin light border, rounded-corner intent
    ''' (WinForms Panel can't do true radius without owner-drawing; a plain border is the
    ''' pragmatic substitute called out as acceptable in the design handoff's fidelity note).</summary>
    Public Sub ApplyCardStyle(panel As Panel)
        panel.BackColor = CardBackground
        panel.Padding = New Padding(16)
        panel.BorderStyle = BorderStyle.FixedSingle
    End Sub

    ''' <summary>Read-only "value" field on the Aperçu screen: light gray fill, thin border.</summary>
    Public Sub ApplyReadOnlyField(textBox As TextBox)
        textBox.ReadOnly = True
        textBox.TabStop = False
        textBox.BackColor = ReadOnlyFill
        textBox.ForeColor = Navy
        textBox.BorderStyle = BorderStyle.FixedSingle
        textBox.Font = BodyFont
    End Sub

    Private Const EM_SETCUEBANNER As Integer = &H1501

    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Private Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As String) As IntPtr
    End Function

    ''' <summary>Shows placeholder/hint text in an empty, unfocused TextBox via the native
    ''' Windows cue-banner (no fake-text-swap hack, no extra library).</summary>
    Public Sub ApplyPlaceholder(textBox As TextBox, placeholderText As String)
        SendMessage(textBox.Handle, EM_SETCUEBANNER, IntPtr.Zero, placeholderText)
    End Sub

    ''' <summary>Loads the embedded Eurocycles logo. Returns Nothing (never throws) if it can't be read.</summary>
    Public Function LoadLogo() As Image
        Try
            Dim asm = Assembly.GetExecutingAssembly()
            Dim resourceName = asm.GetManifestResourceNames().
                FirstOrDefault(Function(n) n.EndsWith("eurocycles-logo.png", StringComparison.OrdinalIgnoreCase))
            If resourceName Is Nothing Then Return Nothing

            Using stream = asm.GetManifestResourceStream(resourceName)
                If stream Is Nothing Then Return Nothing
                Return New Bitmap(Image.FromStream(stream))
            End Using
        Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is IOException
            Return Nothing
        End Try
    End Function

    ''' <summary>Builds the navy branding strip (logo + subtitle) shown at the top of every screen.</summary>
    Public Function BuildHeaderStrip(subtitle As String) As Panel
        Dim strip As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 44,
            .BackColor = Navy,
            .Padding = New Padding(18, 0, 18, 0)
        }

        Dim logo = LoadLogo()
        If logo IsNot Nothing Then
            Dim pic As New PictureBox With {
                .Image = logo,
                .SizeMode = PictureBoxSizeMode.Zoom,
                .Size = New Size(96, 20),
                .Location = New Point(18, 12),
                .BackColor = Color.Transparent
            }
            strip.Controls.Add(pic)
        End If

        Dim label As New Label With {
            .Text = subtitle,
            .ForeColor = HeaderSubtitle,
            .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point(If(logo IsNot Nothing, 122, 18), 14),
            .BackColor = Color.Transparent
        }
        strip.Controls.Add(label)

        Return strip
    End Function

End Module
