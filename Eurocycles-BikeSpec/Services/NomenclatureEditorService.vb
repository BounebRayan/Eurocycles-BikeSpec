Imports System.Linq

''' <summary>
''' Orchestrates saving a Nomenclature header together with its BOM lines as a
''' single unit: inserts/updates the header, then reconciles the line list
''' against its original state (insert new lines, update existing ones,
''' delete removed ones).
''' </summary>
Public Class NomenclatureEditorService

    Private ReadOnly _nomenclatureRepository As NomenclatureRepository
    Private ReadOnly _ligneRepository As LigneNomenclatureRepository

    Public Sub New(Optional nomenclatureRepository As NomenclatureRepository = Nothing,
                   Optional ligneRepository As LigneNomenclatureRepository = Nothing)
        _nomenclatureRepository = If(nomenclatureRepository, New NomenclatureRepository())
        _ligneRepository = If(ligneRepository, New LigneNomenclatureRepository())
    End Sub

    Public Sub Save(nomenclature As Nomenclature,
                     currentLines As List(Of LigneNomenclature),
                     originalLineCodes As IReadOnlyCollection(Of String),
                     isNew As Boolean)

        If isNew Then
            _nomenclatureRepository.Insert(nomenclature)
        Else
            _nomenclatureRepository.Update(nomenclature)
        End If

        Dim currentCodes As New HashSet(Of String)(currentLines.Select(Function(l) l.Code))

        For Each code In originalLineCodes
            If Not currentCodes.Contains(code) Then
                _ligneRepository.Delete(code)
            End If
        Next

        For Each line In currentLines
            If originalLineCodes.Contains(line.Code) Then
                _ligneRepository.Update(line)
            Else
                _ligneRepository.Insert(line)
            End If
        Next
    End Sub

End Class
