''' <summary>
''' A single bill-of-materials line item, belonging to one Nomenclature.
''' </summary>
Public Class LigneNomenclature

    Public Property Code As String
    Public Property NomenclatureCode As String
    Public Property Designation As String
    Public Property Quantite As Decimal
    Public Property Prix As Decimal
    Public Property Fabricant As String
    Public Property Imprime As Boolean
    Public Property Observation As String
    Public Property Devise As String

End Class
