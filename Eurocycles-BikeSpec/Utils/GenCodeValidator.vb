Imports System.Text.RegularExpressions

''' <summary>
''' Validates the Nomenclature.GenCode field: must be blank, or exactly 13 digits.
''' </summary>
Public Module GenCodeValidator

    Private ReadOnly Pattern As New Regex("^\d{13}$", RegexOptions.Compiled)

    Public Function IsValid(value As String) As Boolean
        Return String.IsNullOrEmpty(value) OrElse Pattern.IsMatch(value)
    End Function

End Module
