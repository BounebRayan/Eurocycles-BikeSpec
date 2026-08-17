Imports System.Configuration
Imports Microsoft.Data.SqlClient

''' <summary>
''' Creates connections to the Eurocycles-BikeSpec LocalDB database, using the
''' "EurocyclesBikeSpec" connection string from App.config.
''' </summary>
Public Module SqlConnectionFactory

    Private ReadOnly _connectionString As String =
        ConfigurationManager.ConnectionStrings("EurocyclesBikeSpec")?.ConnectionString

    ''' <summary>
    ''' Creates a new, unopened connection. Caller is responsible for opening
    ''' and disposing it (wrap in a Using block).
    ''' </summary>
    Public Function CreateConnection() As SqlConnection
        If String.IsNullOrEmpty(_connectionString) Then
            Throw New DataAccessException(
                "La configuration de la base de données est introuvable (App.config).")
        End If

        Return New SqlConnection(_connectionString)
    End Function

End Module
