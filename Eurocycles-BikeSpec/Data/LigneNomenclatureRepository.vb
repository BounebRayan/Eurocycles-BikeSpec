Imports Microsoft.Data.SqlClient

''' <summary>
''' Plain ADO.NET CRUD access to the LigneNomenclature table (BOM line items).
''' </summary>
Public Class LigneNomenclatureRepository

    Public Function GetByNomenclatureCode(nomenclatureCode As String) As List(Of LigneNomenclature)
        Dim results As New List(Of LigneNomenclature)

        Const sql As String = "
            SELECT Code, NomenclatureCode, Designation, Quantite, Prix,
                   Fabricant, Imprime, Observation, Devise
            FROM LigneNomenclature
            WHERE NomenclatureCode = @NomenclatureCode
            ORDER BY Code;"

        Using connection = SqlConnectionFactory.CreateConnection()
            Using command As New SqlCommand(sql, connection)
                command.Parameters.AddWithValue("@NomenclatureCode", nomenclatureCode)
                connection.Open()
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        results.Add(Map(reader))
                    End While
                End Using
            End Using
        End Using

        Return results
    End Function

    Public Function GetByCode(code As String) As LigneNomenclature
        Const sql As String = "
            SELECT Code, NomenclatureCode, Designation, Quantite, Prix,
                   Fabricant, Imprime, Observation, Devise
            FROM LigneNomenclature
            WHERE Code = @Code;"

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

        Return Nothing
    End Function

    Public Function Insert(item As LigneNomenclature) As Integer
        Const sql As String = "
            INSERT INTO LigneNomenclature
                (Code, NomenclatureCode, Designation, Quantite, Prix,
                 Fabricant, Imprime, Observation, Devise)
            VALUES
                (@Code, @NomenclatureCode, @Designation, @Quantite, @Prix,
                 @Fabricant, @Imprime, @Observation, @Devise);"

        Using connection = SqlConnectionFactory.CreateConnection()
            Using command As New SqlCommand(sql, connection)
                AddParameters(command, item)
                connection.Open()
                Return command.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Function Update(item As LigneNomenclature) As Integer
        Const sql As String = "
            UPDATE LigneNomenclature
            SET NomenclatureCode = @NomenclatureCode,
                Designation = @Designation,
                Quantite = @Quantite,
                Prix = @Prix,
                Fabricant = @Fabricant,
                Imprime = @Imprime,
                Observation = @Observation,
                Devise = @Devise
            WHERE Code = @Code;"

        Using connection = SqlConnectionFactory.CreateConnection()
            Using command As New SqlCommand(sql, connection)
                AddParameters(command, item)
                connection.Open()
                Return command.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Function Delete(code As String) As Integer
        Const sql As String = "DELETE FROM LigneNomenclature WHERE Code = @Code;"

        Using connection = SqlConnectionFactory.CreateConnection()
            Using command As New SqlCommand(sql, connection)
                command.Parameters.AddWithValue("@Code", code)
                connection.Open()
                Return command.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Private Shared Sub AddParameters(command As SqlCommand, item As LigneNomenclature)
        command.Parameters.AddWithValue("@Code", item.Code)
        command.Parameters.AddWithValue("@NomenclatureCode", item.NomenclatureCode)
        command.Parameters.AddWithValue("@Designation", item.Designation)
        command.Parameters.AddWithValue("@Quantite", item.Quantite)
        command.Parameters.AddWithValue("@Prix", item.Prix)
        command.Parameters.AddWithValue("@Fabricant", If(CObj(item.Fabricant), DBNull.Value))
        command.Parameters.AddWithValue("@Imprime", item.Imprime)
        command.Parameters.AddWithValue("@Observation", If(CObj(item.Observation), DBNull.Value))
        command.Parameters.AddWithValue("@Devise", item.Devise)
    End Sub

    Private Shared Function Map(reader As SqlDataReader) As LigneNomenclature
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

End Class
