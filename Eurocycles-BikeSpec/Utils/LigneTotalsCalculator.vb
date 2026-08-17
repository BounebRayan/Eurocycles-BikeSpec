Imports System.Linq

''' <summary>
''' Computes BOM line totals (Quantite * Prix, summed) grouped by Devise.
''' Shared by FormNomenclature (live totals while editing) and FormApercu
''' (totals on the printable sheet) so both show the same numbers.
''' </summary>
Public Module LigneTotalsCalculator

    Public Function ComputeTotals(lines As IEnumerable(Of LigneNomenclature)) As List(Of (Devise As String, Total As Decimal))
        Return lines.
            GroupBy(Function(l) l.Devise).
            Select(Function(g) (Devise:=g.Key, Total:=g.Sum(Function(l) l.Quantite * l.Prix))).
            OrderBy(Function(t) t.Devise).
            ToList()
    End Function

    ''' <summary>Formats totals as e.g. "Total : 125.500 Euro · 40.000 USD", or an empty string
    ''' when there are no lines (nothing shown, rather than a placeholder message). Uses 3
    ''' decimals uniformly (matching Prix's DECIMAL(10,3) scale) rather than varying by currency.</summary>
    Public Function FormatTotals(lines As IEnumerable(Of LigneNomenclature)) As String
        Dim totals = ComputeTotals(lines)
        If totals.Count = 0 Then Return String.Empty

        Return "Total : " & String.Join(" · ", totals.Select(Function(t) $"{t.Total:N3} {t.Devise}"))
    End Function

End Module
