''' <summary>
''' A bike bill-of-materials header record. Maps 1:1 to the Nomenclature table.
''' </summary>
Public Class Nomenclature

    Public Property Code As String
    Public Property Nom As String
    Public Property [Date] As Date
    Public Property Marque As String
    Public Property GenCode As String
    Public Property NW As Decimal?
    Public Property GW As Decimal?
    Public Property Modele As String
    Public Property FrameSize As String
    Public Property WheelSize As String
    Public Property RefCustomer As String
    Public Property Couleur As String
    Public Property TypeDecor As String
    Public Property Photo As Byte()

    ''' <summary>The BOM line items belonging to this Nomenclature. Empty unless
    ''' populated by NomenclatureRepository.GetByCode (GetAll/Search return headers only).</summary>
    Public Property Lignes As New List(Of LigneNomenclature)

End Class
