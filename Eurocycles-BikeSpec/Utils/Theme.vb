Imports System.IO
Imports System.Linq
Imports System.Reflection

''' <summary>
''' Central color/typography palette for the Eurocycles brand (navy/yellow).
''' Keeps styling consistent across FormListe, FormNomenclature, and FormApercu
''' without duplicating hex values or re-deriving the same control styling
''' in three places.
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

    ''' <summary>Navy header row, white bold header text, a distinct (lighter) navy for the
    ''' selected-row highlight so it doesn't visually blend with the header row above it.</summary>
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
        grid.DefaultCellStyle.SelectionBackColor = HoverFill
        grid.DefaultCellStyle.SelectionForeColor = Navy
        grid.DefaultCellStyle.Padding = New Padding(4, 2, 4, 2)
        grid.AlternatingRowsDefaultCellStyle.BackColor = CardBackground
        grid.RowTemplate.Height = 30
        grid.CellBorderStyle = DataGridViewCellBorderStyle.Single
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

    ''' <summary>Places a caption Label above an input control at a fixed position within a
    ''' card, sized to the given width. Shared by FormNomenclature (editable fields) and
    ''' FormApercu (read-only fields) so both position/space fields identically. Returns the
    ''' Y to use for the next field.</summary>
    Public Function AddField(parent As Panel, label As Label, caption As String, inputControl As Control,
                              x As Integer, y As Integer, width As Integer) As Integer
        label.Text = caption
        label.AutoSize = True
        label.Location = New Point(x, y)
        ApplyFieldLabel(label)
        parent.Controls.Add(label)

        inputControl.Location = New Point(x, y + 18)
        inputControl.Width = width
        inputControl.Font = BodyFont
        If TypeOf inputControl Is TextBox Then
            DirectCast(inputControl, TextBox).BorderStyle = BorderStyle.FixedSingle
        End If
        parent.Controls.Add(inputControl)

        Return y + 18 + inputControl.Height + 14
    End Function

    ''' <summary>Read-only "value" field on the Aperçu screen: light gray fill, thin border.</summary>
    Public Sub ApplyReadOnlyField(textBox As TextBox)
        textBox.ReadOnly = True
        textBox.TabStop = False
        textBox.BackColor = ReadOnlyFill
        textBox.ForeColor = Navy
        textBox.BorderStyle = BorderStyle.FixedSingle
        textBox.Font = BodyFont
    End Sub

    ''' <summary>Wires up placeholder/hint behavior on a TextBox: gray placeholder text when
    ''' empty and not focused, cleared to real (black) input on focus. The native Windows
    ''' cue-banner (EM_SETCUEBANNER) would be simpler, but it silently does nothing on a
    ''' Multiline TextBox - which txtSearch needs to be, to match the search buttons' height -
    ''' so this manual show/hide approach is used instead. Call only once per TextBox (e.g. in
    ''' Form_Load); use ShowPlaceholderIfEmpty afterwards (e.g. after a manual .Clear()) rather
    ''' than calling this again, which would stack duplicate event handlers.</summary>
    Public Sub ApplyPlaceholder(textBox As TextBox, hintText As String)
        AddHandler textBox.Enter, Sub(sender As Object, e As EventArgs) HidePlaceholder(textBox)
        AddHandler textBox.Leave, Sub(sender As Object, e As EventArgs) ShowPlaceholderIfEmpty(textBox, hintText)

        ShowPlaceholderIfEmpty(textBox, hintText)
    End Sub

    ''' <summary>Re-shows the placeholder if the box is now empty and unfocused - needed after
    ''' code clears a TextBox's Text directly (e.g. a "Réinitialiser" button), since that
    ''' doesn't raise Leave.</summary>
    Public Sub ShowPlaceholderIfEmpty(textBox As TextBox, hintText As String)
        If textBox.Text.Length = 0 AndAlso Not textBox.Focused Then
            textBox.Text = hintText
            textBox.ForeColor = PlaceholderText
        End If
    End Sub

    Private Sub HidePlaceholder(textBox As TextBox)
        If textBox.ForeColor = PlaceholderText Then
            textBox.Text = String.Empty
            textBox.ForeColor = Color.Black
        End If
    End Sub

    ''' <summary>Returns a TextBox's real typed text, or an empty string if it's currently
    ''' showing its ApplyPlaceholder hint text (so callers never treat the hint as real input).</summary>
    Public Function GetRealText(textBox As TextBox) As String
        Return If(textBox.ForeColor = PlaceholderText, String.Empty, textBox.Text)
    End Function

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

    ''' <summary>Builds the navy branding strip (logo + subtitle) shown at the top of every screen.
    ''' subtitleLabel is returned so callers whose title changes at runtime (e.g. New vs Edit
    ''' mode) can update its Text later without rebuilding the whole strip.</summary>
    Public Function BuildHeaderStrip(subtitle As String, ByRef subtitleLabel As Label) As Panel
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

        subtitleLabel = New Label With {
            .Text = subtitle,
            .ForeColor = HeaderSubtitle,
            .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular),
            .AutoSize = True,
            .Location = New Point(If(logo IsNot Nothing, 122, 18), 14),
            .BackColor = Color.Transparent
        }
        strip.Controls.Add(subtitleLabel)

        Return strip
    End Function

End Module
