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

    ''' <summary>Formats totals as e.g. "125.50 Euro   40.00 USD", or a placeholder when empty.</summary>
    Public Function FormatTotals(lines As IEnumerable(Of LigneNomenclature)) As String
        Dim totals = ComputeTotals(lines)
        If totals.Count = 0 Then Return "Aucune ligne."

        Return "Totaux : " & String.Join("    ", totals.Select(Function(t) $"{t.Total:N2} {t.Devise}"))
    End Function

End Module
