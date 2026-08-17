''' <summary>
''' Generates a short, unique surrogate code for a new BOM line. Deliberately
''' NOT derived from the parent Nomenclature's Code, so it stays valid even if
''' that Code is edited before the record is first saved (New mode).
''' </summary>
Public Module LigneCodeGenerator

    Public Function NewCode() As String
        Return "L" & Guid.NewGuid().ToString("N").Substring(0, 12).ToUpperInvariant()
    End Function

End Module
