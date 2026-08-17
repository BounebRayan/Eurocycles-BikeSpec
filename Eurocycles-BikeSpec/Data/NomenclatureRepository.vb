Option Strict On
Option Explicit On

Imports Microsoft.Data.SqlClient

''' <summary>
''' Plain ADO.NET data access for Nomenclature and its BOM lines
''' (LigneNomenclature). Insert/Update/Delete operate on the header and its
''' lines together, inside a single transaction, so the two tables never
''' end up out of sync with each other.
''' </summary>
Public Class NomenclatureRepository

    ''' <summary>Returns one page of Nomenclature headers (without their lines or Photo - see
    ''' MapHeaderNoPhoto), ordered by Code, plus the total row count across all pages so the
    ''' caller can compute how many pages exist. pageNumber is 1-based.</summary>
    Public Function GetPage(pageNumber As Integer, pageSize As Integer) As (Items As List(Of Nomenclature), TotalCount As Integer)
        Return GetPageInternal(Nothing, pageNumber, pageSize)
    End Function

    ''' <summary>Same as GetPage, but restricted to Nomenclature headers whose Code, Nom, or
    ''' Marque matches searchTerm.</summary>
    Public Function SearchPage(searchTerm As String, pageNumber As Integer, pageSize As Integer) As (Items As List(Of Nomenclature), TotalCount As Integer)
        Return GetPageInternal(searchTerm, pageNumber, pageSize)
    End Function

    ''' <summary>Shared implementation for GetPage/SearchPage: searchTerm = Nothing means no
    ''' WHERE filter (GetPage's case). Uses COUNT(*) OVER() to get the total row count in the
    ''' same round trip as the page's rows, rather than a separate COUNT(*) query.</summary>
    Private Function GetPageInternal(searchTerm As String, pageNumber As Integer, pageSize As Integer) As (Items As List(Of Nomenclature), TotalCount As Integer)
        Dim results As New List(Of Nomenclature)
        Dim totalCount = 0
        Dim offset = Math.Max(0, pageNumber - 1) * pageSize

        Dim whereClause = If(searchTerm Is Nothing, "", "WHERE Code LIKE @Term OR Nom LIKE @Term OR Marque LIKE @Term")
        Dim sql As String = $"
            SELECT Code, Nom, Date, Marque, GenCode, NW, GW, Modele,
                   FrameSize, WheelSize, RefCustomer, Couleur, TypeDecor,
                   COUNT(*) OVER() AS TotalCount
            FROM Nomenclature
            {whereClause}
            ORDER BY Code
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;"

        Try
            Using connection = DatabaseHelper.CreateConnection()
                Using command As New SqlCommand(sql, connection)
                    If searchTerm IsNot Nothing Then
                        Dim safeTerm = searchTerm.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")
                        command.Parameters.AddWithValue("@Term", "%" & safeTerm & "%")
                    End If
                    command.Parameters.AddWithValue("@Offset", offset)
                    command.Parameters.AddWithValue("@PageSize", pageSize)
                    connection.Open()
                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            results.Add(MapHeaderNoPhoto(reader))
                            If totalCount = 0 Then totalCount = reader.GetInt32(reader.GetOrdinal("TotalCount"))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As SqlException
            Dim message = If(searchTerm Is Nothing, "Impossible de charger la liste des nomenclatures.", "Impossible de rechercher les nomenclatures.")
            Throw New DataAccessException(message, ex)
        End Try

        Return (results, totalCount)
    End Function

    ''' <summary>Returns one Nomenclature by Code, with its Lignes populated. Nothing if not found.</summary>
    Public Function GetByCode(code As String) As Nomenclature
        Const headerSql As String = "
            SELECT Code, Nom, Date, Marque, GenCode, NW, GW, Modele,
                   FrameSize, WheelSize, RefCustomer, Couleur, TypeDecor, Photo
            FROM Nomenclature
            WHERE Code = @Code;"

        Const linesSql As String = "
            SELECT Code, NomenclatureCode, Designation, Quantite, Prix,
                   Fabricant, Imprime, Observation, Devise
            FROM LigneNomenclature
            WHERE NomenclatureCode = @NomenclatureCode
            ORDER BY Code;"

        Try
            Using connection = DatabaseHelper.CreateConnection()
                connection.Open()

                Dim result As Nomenclature = Nothing
                Using command As New SqlCommand(headerSql, connection)
                    command.Parameters.AddWithValue("@Code", code)
                    Using reader = command.ExecuteReader(CommandBehavior.SingleRow)
                        If reader.Read() Then
                            result = MapHeader(reader)
                        End If
                    End Using
                End Using

                If result Is Nothing Then Return Nothing

                Using command As New SqlCommand(linesSql, connection)
                    command.Parameters.AddWithValue("@NomenclatureCode", code)
                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            result.Lignes.Add(MapLine(reader))
                        End While
                    End Using
                End Using

                Return result
            End Using
        Catch ex As SqlException
            Throw New DataAccessException("Impossible de charger la nomenclature.", ex)
        End Try
    End Function

    ''' <summary>Inserts the Nomenclature header and all of its Lignes in a single transaction.</summary>
    Public Sub Insert(n As Nomenclature)
        Const headerSql As String = "
            INSERT INTO Nomenclature
                (Code, Nom, Date, Marque, GenCode, NW, GW, Modele,
                 FrameSize, WheelSize, RefCustomer, Couleur, TypeDecor, Photo)
            VALUES
                (@Code, @Nom, @Date, @Marque, @GenCode, @NW, @GW, @Modele,
                 @FrameSize, @WheelSize, @RefCustomer, @Couleur, @TypeDecor, @Photo);"

        Using connection = DatabaseHelper.CreateConnection()
            connection.Open()
            Using transaction = connection.BeginTransaction()
                Try
                    Using command As New SqlCommand(headerSql, connection, transaction)
                        AddHeaderParameters(command, n)
                        command.ExecuteNonQuery()
                    End Using

                    InsertLines(connection, transaction, n)

                    transaction.Commit()
                Catch ex As SqlException
                    Throw New DataAccessException("Impossible d'enregistrer la nomenclature.", ex)
                End Try
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Updates the Nomenclature header, and replaces its Lignes (delete existing, then
    ''' re-insert the current set), all in a single transaction.
    ''' </summary>
    Public Sub Update(n As Nomenclature)
        Const headerSql As String = "
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

        Const deleteLinesSql As String = "DELETE FROM LigneNomenclature WHERE NomenclatureCode = @NomenclatureCode;"

        Using connection = DatabaseHelper.CreateConnection()
            connection.Open()
            Using transaction = connection.BeginTransaction()
                Try
                    Using command As New SqlCommand(headerSql, connection, transaction)
                        AddHeaderParameters(command, n)
                        command.ExecuteNonQuery()
                    End Using

                    Using command As New SqlCommand(deleteLinesSql, connection, transaction)
                        command.Parameters.AddWithValue("@NomenclatureCode", n.Code)
                        command.ExecuteNonQuery()
                    End Using

                    InsertLines(connection, transaction, n)

                    transaction.Commit()
                Catch ex As SqlException
                    Throw New DataAccessException("Impossible de mettre à jour la nomenclature.", ex)
                End Try
            End Using
        End Using
    End Sub

    ''' <summary>Deletes the Nomenclature's Lignes, then the header itself, in a single transaction.</summary>
    Public Sub Delete(code As String)
        Const deleteLinesSql As String = "DELETE FROM LigneNomenclature WHERE NomenclatureCode = @NomenclatureCode;"
        Const deleteHeaderSql As String = "DELETE FROM Nomenclature WHERE Code = @Code;"

        Using connection = DatabaseHelper.CreateConnection()
            connection.Open()
            Using transaction = connection.BeginTransaction()
                Try
                    Using command As New SqlCommand(deleteLinesSql, connection, transaction)
                        command.Parameters.AddWithValue("@NomenclatureCode", code)
                        command.ExecuteNonQuery()
                    End Using

                    Using command As New SqlCommand(deleteHeaderSql, connection, transaction)
                        command.Parameters.AddWithValue("@Code", code)
                        command.ExecuteNonQuery()
                    End Using

                    transaction.Commit()
                Catch ex As SqlException
                    Throw New DataAccessException("Impossible de supprimer la nomenclature.", ex)
                End Try
            End Using
        End Using
    End Sub

    ' --- helpers ---

    Private Shared Sub InsertLines(connection As SqlConnection, transaction As SqlTransaction, n As Nomenclature)
        Const lineSql As String = "
            INSERT INTO LigneNomenclature
                (Code, NomenclatureCode, Designation, Quantite, Prix,
                 Fabricant, Imprime, Observation, Devise)
            VALUES
                (@Code, @NomenclatureCode, @Designation, @Quantite, @Prix,
                 @Fabricant, @Imprime, @Observation, @Devise);"

        For Each line In n.Lignes
            line.NomenclatureCode = n.Code
            Using command As New SqlCommand(lineSql, connection, transaction)
                command.Parameters.AddWithValue("@Code", line.Code)
                command.Parameters.AddWithValue("@NomenclatureCode", line.NomenclatureCode)
                command.Parameters.AddWithValue("@Designation", line.Designation)
                command.Parameters.AddWithValue("@Quantite", line.Quantite)
                command.Parameters.AddWithValue("@Prix", line.Prix)
                command.Parameters.AddWithValue("@Fabricant", If(CObj(line.Fabricant), DBNull.Value))
                command.Parameters.AddWithValue("@Imprime", line.Imprime)
                command.Parameters.AddWithValue("@Observation", If(CObj(line.Observation), DBNull.Value))
                command.Parameters.AddWithValue("@Devise", line.Devise)
                command.ExecuteNonQuery()
            End Using
        Next
    End Sub

    Private Shared Sub AddHeaderParameters(command As SqlCommand, n As Nomenclature)
        command.Parameters.AddWithValue("@Code", n.Code)
        command.Parameters.AddWithValue("@Nom", n.Nom)
        command.Parameters.AddWithValue("@Date", n.Date)
        command.Parameters.AddWithValue("@Marque", If(CObj(n.Marque), DBNull.Value))
        command.Parameters.AddWithValue("@GenCode", If(CObj(n.GenCode), DBNull.Value))
        command.Parameters.AddWithValue("@NW", If(CObj(n.NW), DBNull.Value))
        command.Parameters.AddWithValue("@GW", If(CObj(n.GW), DBNull.Value))
        command.Parameters.AddWithValue("@Modele", If(CObj(n.Modele), DBNull.Value))
        command.Parameters.AddWithValue("@FrameSize", If(CObj(n.FrameSize), DBNull.Value))
        command.Parameters.AddWithValue("@WheelSize", If(CObj(n.WheelSize), DBNull.Value))
        command.Parameters.AddWithValue("@RefCustomer", If(CObj(n.RefCustomer), DBNull.Value))
        command.Parameters.AddWithValue("@Couleur", If(CObj(n.Couleur), DBNull.Value))
        command.Parameters.AddWithValue("@TypeDecor", If(CObj(n.TypeDecor), DBNull.Value))

        ' AddWithValue can't be used here: when n.Photo is Nothing, it would pass DBNull.Value
        ' with no informative .NET type to infer from, so SqlClient defaults the parameter to
        ' NVarChar - which SQL Server then refuses to implicitly convert into VARBINARY(MAX),
        ' throwing "Implicit conversion from data type nvarchar to varbinary(max) is not
        ' allowed" on every Insert/Update of a record with no photo. Explicit SqlDbType avoids
        ' the ambiguous inference entirely.
        Dim photoParam = command.Parameters.Add("@Photo", SqlDbType.VarBinary, -1)
        photoParam.Value = If(CObj(n.Photo), DBNull.Value)
    End Sub

    Private Shared Function MapHeader(reader As SqlDataReader) As Nomenclature
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

    ''' <summary>Same as MapHeader, minus Photo - used for GetPage/SearchPage's list rows, which
    ''' don't have a Photo column in their SELECT (see GetPageInternal) since the list grid never
    ''' displays it and shipping VARBINARY(MAX) blobs for every row on every page load would be
    ''' wasteful. FormNomenclature re-fetches the full record (via GetByCode) before editing, so
    ''' this never causes Photo to be silently dropped when a record is saved.</summary>
    Private Shared Function MapHeaderNoPhoto(reader As SqlDataReader) As Nomenclature
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
            .TypeDecor = GetNullableString(reader, "TypeDecor")
        }
    End Function

    Private Shared Function MapLine(reader As SqlDataReader) As LigneNomenclature
        Return New LigneNomenclature With {
            .Code = reader.GetString(reader.GetOrdinal("Code")),
            .NomenclatureCode = reader.GetString(reader.GetOrdinal("NomenclatureCode")),
            .Designation = reader.GetString(reader.GetOrdinal("Designation")),
            .Quantite = reader.GetDecimal(reader.GetOrdinal("Quantite")),
            .Prix = reader.GetDecimal(reader.GetOrdinal("Prix")),
            .Fabricant = GetNullableString(reader, "Fabricant"),
            .Imprime = reader.GetBoolean(reader.GetOrdinal("Imprime")),
            .Observation = GetNullableString(reader, "Observation"),
            .Devise = reader.GetString(reader.GetOrdinal("Devise"))
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
