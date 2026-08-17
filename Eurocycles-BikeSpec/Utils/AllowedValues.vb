''' <summary>
''' Single source of truth for the value lists the UI offers via DropDownList
''' ComboBoxes. These MUST stay in sync with the CHECK constraints in
''' SQLQuery1.sql (CK_Nomenclature_FrameSize, CK_Nomenclature_WheelSize,
''' CK_Nomenclature_TypeDecor, CK_LigneNomenclature_Devise) — if the DB
''' constraint list changes, update it here too, so the UI never lets a user
''' pick a value the database will then reject.
''' </summary>
Public Module AllowedValues

    Public ReadOnly FrameSizes As String() = {"12", "14", "15", "16", "17"}
    Public ReadOnly WheelSizes As String() = {"12", "14", "16", "18", "20", "24", "27", "29"}
    Public ReadOnly TypeDecors As String() = {"Standard", "Polyester", "Water Transfer"}
    Public ReadOnly Devises As String() = {"Euro", "USD", "TND", "YEN"}

End Module
