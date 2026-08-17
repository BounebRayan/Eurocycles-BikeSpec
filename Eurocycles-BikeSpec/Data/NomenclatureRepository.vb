Imports Microsoft.Data.SqlClient

''' <summary>
''' Plain ADO.NET CRUD access to the Nomenclature table.
''' </summary>
Public Class NomenclatureRepository

    Public Function GetAll() As List(Of Nomenclature)
        Dim results As New List(Of Nomenclature)

        Const sql As String = "
            SELECT Code, Nom, Date, Marque, GenCode, NW, GW, Modele,
                   FrameSize, WheelSize, RefCustomer, Couleur, TypeDecor, Photo
            FROM Nomenclature
            ORDER BY Code;"

        Try
            Using connection = SqlConnectionFactory.CreateConnection()
                Using command As New SqlCommand(sql, connection)
                    connection.Open()
                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            results.Add(Map(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As SqlException
            Throw New DataAccessException("Impossible de charger la liste des nomenclatures.", ex)
        End Try

        Return results
    End Function

    Public Function Search(term As String) As List(Of Nomenclature)
        Dim results As New List(Of Nomenclature)

        Const sql As String = "
            SELECT Code, Nom, Date, Marque, GenCode, NW, GW, Modele,
                   FrameSize, WheelSize, RefCustomer, Couleur, TypeDecor, Photo
            FROM Nomenclature
            WHERE Code LIKE @Term OR Nom LIKE @Term OR Marque LIKE @Term
            ORDER BY Code;"

        Try
            Using connection = SqlConnectionFactory.CreateConnection()
                Using command As New SqlCommand(sql, connection)
                    command.Parameters.AddWithValue("@Term", "%" & term & "%")
                    connection.Open()
                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            results.Add(Map(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As SqlException
            Throw New DataAccessException("Impossible de rechercher les nomenclatures.", ex)
        End Try

        Return results
    End Function

    Public Function GetByCode(code As String) As Nomenclature
        Const sql As String = "
            SELECT Code, Nom, Date, Marque, GenCode, NW, GW, Modele,
                   FrameSize, WheelSize, RefCustomer, Couleur, TypeDecor, Photo
            FROM Nomenclature
            WHERE Code = @Code;"

        Try
            Using connection = SqlConnectionFactory.CreateConnection()
                Using command As New SqlCommand(sql, connection)
                    command.Parameters.AddWithValue("@Code", code)
                    connection.Open()
                    Using reader = command.ExecuteReader(CommandBehavior.SingleRow)
                        If reader.Read() Then
                            Return Map(reader)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As SqlException
            Throw New DataAccessException("Impossible de charger la nomenclature.", ex)
        End Try

        Return Nothing
    End Function

    Public Function Exists(code As String) As Boolean
        Const sql As String = "SELECT 1 FROM Nomenclature WHERE Code = @Code;"

        Try
            Using connection = SqlConnectionFactory.CreateConnection()
                Using command As New SqlCommand(sql, connection)
                    command.Parameters.AddWithValue("@Code", code)
                    connection.Open()
                    Return command.ExecuteScalar() IsNot Nothing
                End Using
            End Using
        Catch ex As SqlException
            Throw New DataAccessException("Impossible de vérifier la nomenclature.", ex)
        End Try
    End Function

    Public Function Insert(item As Nomenclature) As Integer
        Const sql As String = "
            INSERT INTO Nomenclature
                (Code, Nom, Date, Marque, GenCode, NW, GW, Modele,
                 FrameSize, WheelSize, RefCustomer, Couleur, TypeDecor, Photo)
            VALUES
                (@Code, @Nom, @Date, @Marque, @GenCode, @NW, @GW, @Modele,
                 @FrameSize, @WheelSize, @RefCustomer, @Couleur, @TypeDecor, @Photo);"

        Try
            Using connection = SqlConnectionFactory.CreateConnection()
                Using command As New SqlCommand(sql, connection)
                    AddParameters(command, item)
                    connection.Open()
                    Return command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As SqlException
            Throw New DataAccessException("Impossible d'enregistrer la nomenclature.", ex)
        End Try
    End Function

    Public Function Update(item As Nomenclature) As Integer
        Const sql As String = "
            UPDATE Nomenclature
            SET Nom = @Nom,
                Date = @Date,
                Marque = @Marque,
                GenCode = @GenCode,
                NW = @NW,
                GW = @GW,
                Modele = @Modele,
                FrameSize = @FrameSize,
                WheelSize = @WheelSize,
                RefCustomer = @RefCustomer,
                Couleur = @Couleur,
                TypeDecor = @TypeDecor,
                Photo = @Photo
            WHERE Code = @Code;"

        Try
            Using connection = SqlConnectionFactory.CreateConnection()
                Using command As New SqlCommand(sql, connection)
                    AddParameters(command, item)
                    connection.Open()
                    Return command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As SqlException
            Throw New DataAccessException("Impossible de mettre à jour la nomenclature.", ex)
        End Try
    End Function

    Public Function Delete(code As String) As Integer
        Const sql As String = "DELETE FROM Nomenclature WHERE Code = @Code;"

        Try
            Using connection = SqlConnectionFactory.CreateConnection()
                Using command As New SqlCommand(sql, connection)
                    command.Parameters.AddWithValue("@Code", code)
                    connection.Open()
                    Return command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As SqlException
            Throw New DataAccessException("Impossible de supprimer la nomenclature.", ex)
        End Try
    End Function

    Private Shared Sub AddParameters(command As SqlCommand, item As Nomenclature)
        command.Parameters.AddWithValue("@Code", item.Code)
        command.Parameters.AddWithValue("@Nom", item.Nom)
        command.Parameters.AddWithValue("@Date", item.Date)
        command.Parameters.AddWithValue("@Marque", If(CObj(item.Marque), DBNull.Value))
        command.Parameters.AddWithValue("@GenCode", If(CObj(item.GenCode), DBNull.Value))
        command.Parameters.AddWithValue("@NW", If(CObj(item.NW), DBNull.Value))
        command.Parameters.AddWithValue("@GW", If(CObj(item.GW), DBNull.Value))
        command.Parameters.AddWithValue("@Modele", If(CObj(item.Modele), DBNull.Value))
        command.Parameters.AddWithValue("@FrameSize", If(CObj(item.FrameSize), DBNull.Value))
        command.Parameters.AddWithValue("@WheelSize", If(CObj(item.WheelSize), DBNull.Value))
        command.Parameters.AddWithValue("@RefCustomer", If(CObj(item.RefCustomer), DBNull.Value))
        command.Parameters.AddWithValue("@Couleur", If(CObj(item.Couleur), DBNull.Value))
        command.Parameters.AddWithValue("@TypeDecor", If(CObj(item.TypeDecor), DBNull.Value))
        command.Parameters.AddWithValue("@Photo", If(CObj(item.Photo), DBNull.Value))
    End Sub

    Private Shared Function Map(reader As SqlDataReader) As Nomenclature
        Return New Nomenclature With {
            .Code = reader.GetString(reader.GetOrdinal("Code")),
            .Nom = reader.GetString(reader.GetOrdinal("Nom")),
            .Date = reader.GetDateTime(reader.GetOrdinal("Date")),
            .Marque = GetNullableString(reader, "Marque"),
            .GenCode = GetNullableString(reader, "GenCode"),
            .NW = GetNullableDecimal(reader, "NW"),
            .GW = GetNullableDecimal(reader, "GW"),
            .Modele = GetNullableString(reader, "Modele"),
            .FrameSize = GetNullableString(reader, "FrameSize"),
            .WheelSize = GetNullableString(reader, "WheelSize"),
            .RefCustomer = GetNullableString(reader, "RefCustomer"),
            .Couleur = GetNullableString(reader, "Couleur"),
            .TypeDecor = GetNullableString(reader, "TypeDecor"),
            .Photo = GetNullableBytes(reader, "Photo")
        }
    End Function

    Private Shared Function GetNullableString(reader As SqlDataReader, columnName As String) As String
        Dim ordinal = reader.GetOrdinal(columnName)
        Return If(reader.IsDBNull(ordinal), Nothing, reader.GetString(ordinal))
    End Function

    Private Shared Function GetNullableDecimal(reader As SqlDataReader, columnName As String) As Decimal?
        Dim ordinal = reader.GetOrdinal(columnName)
        Return If(reader.IsDBNull(ordinal), CType(Nothing, Decimal?), reader.GetDecimal(ordinal))
    End Function

    Private Shared Function GetNullableBytes(reader As SqlDataReader, columnName As String) As Byte()
        Dim ordinal = reader.GetOrdinal(columnName)
        Return If(reader.IsDBNull(ordinal), Nothing, CType(reader.GetValue(ordinal), Byte()))
    End Function

End Class
