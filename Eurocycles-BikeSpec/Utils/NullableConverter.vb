Imports System.Globalization

''' <summary>
''' Shared helpers for converting between blank-means-NULL form fields and
''' nullable model values. Used by FormNomenclature (parsing input) and
''' FormApercu (formatting for display).
''' </summary>
Public Module NullableConverter

    Public Function NullIfEmpty(value As String) As String
        Dim trimmed = value?.Trim()
        Return If(String.IsNullOrEmpty(trimmed), Nothing, trimmed)
    End Function

    Public Function ParseNullableDecimal(text As String) As Decimal?
        Dim trimmed = text?.Trim()
        If String.IsNullOrEmpty(trimmed) Then Return Nothing
        Dim result As Decimal
        If Decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.CurrentCulture, result) Then
            Return result
        End If
        Return Nothing
    End Function

    ''' <summary>Formats a nullable decimal for read-only display; blank when Nothing.</summary>
    Public Function FormatNullableDecimal(value As Decimal?) As String
        Return If(value.HasValue, value.Value.ToString(CultureInfo.CurrentCulture), String.Empty)
    End Function

    ''' <summary>Formats a nullable string for read-only display; a placeholder dash when blank.</summary>
    Public Function FormatOrDash(value As String) As String
        Return If(String.IsNullOrEmpty(value), "-", value)
    End Function

End Module
