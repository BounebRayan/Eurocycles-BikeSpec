Imports System.Linq

''' <summary>
''' Computes BOM line totals (Quantite * Prix, summed) grouped by Devise. Used by
''' CurrencyFormatter.FormatTotals, which both FormNomenclature (live totals while editing)
''' and FormApercu (totals on the printable sheet) call to render those totals, so both
''' show the same numbers in the same currency-formatted style.
''' </summary>
Public Module LigneTotalsCalculator

    Public Function ComputeTotals(lines As IEnumerable(Of LigneNomenclature)) As List(Of (Devise As String, Total As Decimal))
        Return lines.
            GroupBy(Function(l) l.Devise).
            Select(Function(g) (Devise:=g.Key, Total:=g.Sum(Function(l) l.Quantite * l.Prix))).
            OrderBy(Function(t) t.Devise).
            ToList()
    End Function

End Module
