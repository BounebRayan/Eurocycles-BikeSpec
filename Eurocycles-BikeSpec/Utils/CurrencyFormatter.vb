Imports System.Globalization

''' <summary>
''' Formats monetary amounts for read-only display (FormApercu and its print layout only -
''' FormNomenclature keeps Devise as plain editable text "Euro"/"USD"/"TND"/"YEN", matching
''' the DB's allowed values in AllowedValues.Devises). Each currency gets its real-world
''' symbol, its own decimal/group separator convention, and the symbol's usual position,
''' instead of one generic "N3 CODE" format for every currency.
''' </summary>
Public Module CurrencyFormatter

    ''' <summary>Formats one amount using the given currency's display convention, e.g.
    ''' 1234.5D + "Euro" -> "1.234,500 €", 1234.5D + "USD" -> "$1,234.500".
    ''' Falls back to the Euro style for any currency not in AllowedValues.Devises.</summary>
    Public Function FormatAmount(amount As Decimal, devise As String) As String
        Dim style = StyleFor(devise)

        Dim nfi As New NumberFormatInfo() With {
            .NumberDecimalDigits = 3,
            .NumberDecimalSeparator = style.DecimalSeparator,
            .NumberGroupSeparator = style.GroupSeparator
        }
        Dim number = amount.ToString("N3", nfi)

        Return If(style.SymbolBeforeAmount, $"{style.Symbol}{number}", $"{number} {style.Symbol}")
    End Function

    ''' <summary>Same grouping as LigneTotalsCalculator.FormatTotals (one subtotal per Devise,
    ''' ordered by Devise), but each subtotal is rendered with FormatAmount instead of a plain
    ''' "N3 CODE" pair. Empty string when there are no lines.</summary>
    Public Function FormatTotals(lines As IEnumerable(Of LigneNomenclature)) As String
        Dim totals = LigneTotalsCalculator.ComputeTotals(lines)
        If totals.Count = 0 Then Return String.Empty

        Return "Total : " & String.Join(" · ", totals.Select(Function(t) FormatAmount(t.Total, t.Devise)))
    End Function

    Private Function StyleFor(devise As String) As (Symbol As String, SymbolBeforeAmount As Boolean, DecimalSeparator As String, GroupSeparator As String)
        Select Case devise
            Case "USD"
                Return ("$", True, ".", ",")
            Case "YEN"
                Return ("¥", True, ".", ",")
            Case "TND"
                Return ("DT", False, ",", ".")
            Case Else ' "Euro" and anything unrecognized fall back to Euro-style formatting
                Return ("€", False, ",", ".")
        End Select
    End Function

End Module
