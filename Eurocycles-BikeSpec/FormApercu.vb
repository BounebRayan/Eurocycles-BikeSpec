Imports System.Drawing.Printing
Imports System.Linq

''' <summary>
''' Read-only "fiche technique" preview of a Nomenclature and its BOM lines,
''' with a simple manual-layout print/print-preview.
''' </summary>
Public Class FormApercu

    Private ReadOnly _nomenclature As Nomenclature
    Private ReadOnly _lignes As List(Of LigneNomenclature)
    Private WithEvents _printDocument As New PrintDocument()
    Private _printLineIndex As Integer

    Public Sub New(nomenclature As Nomenclature, lignes As List(Of LigneNomenclature))
        InitializeComponent()
        If nomenclature Is Nothing Then Throw New ArgumentNullException(NameOf(nomenclature))
        _nomenclature = nomenclature
        _lignes = If(lignes, New List(Of LigneNomenclature)())
        Me.Text = $"Aperçu - {nomenclature.Code}"
    End Sub

    Private Sub FormApercu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCode.Text = _nomenclature.Code
        txtNom.Text = _nomenclature.Nom
        txtDate.Text = _nomenclature.Date.ToString("dd/MM/yyyy")
        txtMarque.Text = NullableConverter.FormatOrDash(_nomenclature.Marque)
        txtModele.Text = NullableConverter.FormatOrDash(_nomenclature.Modele)
        txtGenCode.Text = NullableConverter.FormatOrDash(_nomenclature.GenCode)
        txtNW.Text = NullableConverter.FormatOrDash(NullableConverter.FormatNullableDecimal(_nomenclature.NW))
        txtGW.Text = NullableConverter.FormatOrDash(NullableConverter.FormatNullableDecimal(_nomenclature.GW))
        txtFrameSize.Text = NullableConverter.FormatOrDash(_nomenclature.FrameSize)
        txtWheelSize.Text = NullableConverter.FormatOrDash(_nomenclature.WheelSize)
        txtRefCustomer.Text = NullableConverter.FormatOrDash(_nomenclature.RefCustomer)
        txtCouleur.Text = NullableConverter.FormatOrDash(_nomenclature.Couleur)
        txtTypeDecor.Text = NullableConverter.FormatOrDash(_nomenclature.TypeDecor)

        picPhoto.Image = PhotoHelper.TryLoadImage(_nomenclature.Photo)

        dgvLignes.DataSource = _lignes
        lblTotaux.Text = LigneTotalsCalculator.FormatTotals(_lignes)
    End Sub

    Private Sub FormApercu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        picPhoto.Image?.Dispose()
        _printDocument.Dispose()
    End Sub

    Private Sub btnFermer_Click(sender As Object, e As EventArgs) Handles btnFermer.Click
        Me.Close()
    End Sub

    Private Sub btnImprimer_Click(sender As Object, e As EventArgs) Handles btnImprimer.Click
        Try
            _printLineIndex = 0
            Using preview As New PrintPreviewDialog()
                preview.Document = _printDocument
                preview.Width = 950
                preview.Height = 700
                preview.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show($"Impossible d'afficher l'aperçu avant impression : {ex.Message}",
                             "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- Manual print layout: header block (first page only) + a simple line table that
    ' paginates when it doesn't fit. Deliberately basic, per spec ("doesn't need to be fancy").

    Private Sub _printDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles _printDocument.PrintPage
        Dim g = e.Graphics
        Dim left = CSng(e.MarginBounds.Left)
        Dim right = CSng(e.MarginBounds.Right)
        Dim bottom = CSng(e.MarginBounds.Bottom)
        Dim y = CSng(e.MarginBounds.Top)

        Using titleFont As New Font("Segoe UI", 14, FontStyle.Bold),
              headerFont As New Font("Segoe UI", 9, FontStyle.Bold),
              normalFont As New Font("Segoe UI", 9, FontStyle.Regular),
              totalsFont As New Font("Segoe UI", 9, FontStyle.Bold)

            Dim lineHeight = normalFont.GetHeight(g) + 4
            Dim columns = BuildColumnLayout(left, e.MarginBounds.Width)

            If _printLineIndex = 0 Then
                y = DrawHeaderBlock(g, titleFont, normalFont, left, right, y)
                g.DrawLine(Pens.Black, left, y, right, y)
                y += 8
            End If

            DrawRow(g, headerFont, columns, y, Nothing)
            y += lineHeight
            g.DrawLine(Pens.Black, left, y, right, y)
            y += 4

            While _printLineIndex < _lignes.Count
                If y + lineHeight > bottom Then
                    e.HasMorePages = True
                    Return
                End If

                DrawRow(g, normalFont, columns, y, _lignes(_printLineIndex))
                y += lineHeight
                _printLineIndex += 1
            End While

            y += 6
            g.DrawString(LigneTotalsCalculator.FormatTotals(_lignes), totalsFont, Brushes.Black, left, y)
        End Using

        e.HasMorePages = False
        _printLineIndex = 0
    End Sub

    Private Function DrawHeaderBlock(g As Graphics, titleFont As Font, normalFont As Font,
                                      left As Single, right As Single, startY As Single) As Single
        Dim y = startY
        g.DrawString($"Fiche technique - {_nomenclature.Code}", titleFont, Brushes.Black, left, y)
        y += titleFont.GetHeight(g) + 10

        Const photoSize = 140
        Dim fieldsTop = y
        Dim lineHeight = normalFont.GetHeight(g) + 4

        For Each field In BuildHeaderFieldList()
            g.DrawString($"{field.Label} : {field.Value}", normalFont, Brushes.Black, left, y)
            y += lineHeight
        Next

        Dim img = PhotoHelper.TryLoadImage(_nomenclature.Photo)
        If img IsNot Nothing Then
            Try
                g.DrawImage(img, New Rectangle(CInt(right - photoSize), CInt(fieldsTop), photoSize, photoSize))
            Finally
                img.Dispose()
            End Try
        End If

        Return Math.Max(y, fieldsTop + photoSize) + 10
    End Function

    Private Function BuildHeaderFieldList() As List(Of (Label As String, Value As String))
        Return New List(Of (Label As String, Value As String)) From {
            ("Code", _nomenclature.Code),
            ("Nom", _nomenclature.Nom),
            ("Date", _nomenclature.Date.ToString("dd/MM/yyyy")),
            ("Marque", NullableConverter.FormatOrDash(_nomenclature.Marque)),
            ("Modèle", NullableConverter.FormatOrDash(_nomenclature.Modele)),
            ("GenCode", NullableConverter.FormatOrDash(_nomenclature.GenCode)),
            ("NW (kg)", NullableConverter.FormatOrDash(NullableConverter.FormatNullableDecimal(_nomenclature.NW))),
            ("GW (kg)", NullableConverter.FormatOrDash(NullableConverter.FormatNullableDecimal(_nomenclature.GW))),
            ("Taille cadre", NullableConverter.FormatOrDash(_nomenclature.FrameSize)),
            ("Taille roue", NullableConverter.FormatOrDash(_nomenclature.WheelSize)),
            ("Réf. client", NullableConverter.FormatOrDash(_nomenclature.RefCustomer)),
            ("Couleur", NullableConverter.FormatOrDash(_nomenclature.Couleur)),
            ("Type décor", NullableConverter.FormatOrDash(_nomenclature.TypeDecor))
        }
    End Function

    Private Function BuildColumnLayout(left As Single, totalWidth As Integer) As List(Of (Header As String, X As Single, Width As Single))
        Dim headers = New String() {"Désignation", "Qté", "Prix", "Fabricant", "Imprimé", "Devise", "Observation"}
        Dim weights = New Single() {0.26F, 0.09F, 0.11F, 0.16F, 0.08F, 0.08F, 0.22F}

        Dim result As New List(Of (Header As String, X As Single, Width As Single))
        Dim x = left
        For i = 0 To headers.Length - 1
            Dim w = totalWidth * weights(i)
            result.Add((headers(i), x, w))
            x += w
        Next
        Return result
    End Function

    ''' <summary>Draws either the column header row (line = Nothing) or one data row.</summary>
    Private Sub DrawRow(g As Graphics, font As Font, columns As List(Of (Header As String, X As Single, Width As Single)),
                         y As Single, line As LigneNomenclature)
        Dim values As String()
        If line Is Nothing Then
            values = columns.Select(Function(c) c.Header).ToArray()
        Else
            values = New String() {
                line.Designation,
                line.Quantite.ToString("N2"),
                line.Prix.ToString("N3"),
                NullableConverter.FormatOrDash(line.Fabricant),
                If(line.Imprime, "Oui", "Non"),
                line.Devise,
                NullableConverter.FormatOrDash(line.Observation)
            }
        End If

        For i = 0 To columns.Count - 1
            Dim rect As New RectangleF(columns(i).X, y, columns(i).Width - 4, font.GetHeight(g) + 4)
            g.DrawString(values(i), font, Brushes.Black, rect)
        Next
    End Sub

End Class
