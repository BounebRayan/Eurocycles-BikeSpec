''' <summary>
''' Wraps low-level ADO.NET/SQL failures so the UI layer can show a friendly
''' message instead of a raw SqlException.
''' </summary>
Public Class DataAccessException
    Inherits Exception

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

End Class
