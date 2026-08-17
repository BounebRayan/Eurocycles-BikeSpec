CREATE TABLE Nomenclature (
    Code            NVARCHAR(20)    NOT NULL PRIMARY KEY,
    Nom             NVARCHAR(100)   NOT NULL,
    Date            DATE            NOT NULL,
    Marque          NVARCHAR(50)    NULL,
    GenCode         CHAR(13)        NULL,
    NW              DECIMAL(10,3)   NULL,
    GW              DECIMAL(10,3)   NULL,
    Modele          NVARCHAR(50)    NULL,
    FrameSize       NVARCHAR(5)     NULL,
    WheelSize       NVARCHAR(5)     NULL,
    RefCustomer     NVARCHAR(50)    NULL,
    Couleur         NVARCHAR(50)    NULL,
    TypeDecor       NVARCHAR(20)    NULL,
    Photo           VARBINARY(MAX)  NULL,

    CONSTRAINT CK_Nomenclature_GenCode
        CHECK (GenCode IS NULL OR (LEN(GenCode) = 13 AND GenCode NOT LIKE '%[^0-9]%')),
    CONSTRAINT CK_Nomenclature_FrameSize
        CHECK (FrameSize IN ('12','14','15','16','17')),
    CONSTRAINT CK_Nomenclature_WheelSize
        CHECK (WheelSize IN ('12','14','16','18','20','24','27','29')),
    CONSTRAINT CK_Nomenclature_TypeDecor
        CHECK (TypeDecor IN ('Standard','Polyester','Water Transfer'))
);
GO

CREATE TABLE LigneNomenclature (
    Code                NVARCHAR(20)    NOT NULL PRIMARY KEY,
    NomenclatureCode    NVARCHAR(20)    NOT NULL,
    Designation         NVARCHAR(150)   NOT NULL,
    Quantite            DECIMAL(10,2)   NOT NULL,
    Prix                DECIMAL(10,3)   NOT NULL,
    Fabricant           NVARCHAR(100)   NULL,
    Imprime             BIT             NOT NULL DEFAULT 0,
    Observation         NVARCHAR(255)   NULL,
    Devise              NVARCHAR(10)    NOT NULL,

    CONSTRAINT FK_LigneNomenclature_Nomenclature
        FOREIGN KEY (NomenclatureCode) REFERENCES Nomenclature(Code)
        ON DELETE CASCADE,
    CONSTRAINT CK_LigneNomenclature_Devise
        CHECK (Devise IN ('Euro','USD','TND','YEN'))
);
GO

CREATE INDEX IX_LigneNomenclature_NomenclatureCode
    ON LigneNomenclature(NomenclatureCode);
GO