-- Sample data for local development/testing of Eurocycles-BikeSpec.
-- Safe to re-run: clears existing rows first (lines before headers, respecting the FK).

DELETE FROM LigneNomenclature;
DELETE FROM Nomenclature;
GO

INSERT INTO Nomenclature
    (Code, Nom, Date, Marque, GenCode, NW, GW, Modele, FrameSize, WheelSize, RefCustomer, Couleur, TypeDecor, Photo)
VALUES
    ('BK-2024-001', 'VTT Alpin Pro', '2024-03-15', 'Eurocycles', '1234567890123', 12.500, 14.800,
        'Alpin Pro X1', '17', '27', 'CUST-001', 'Rouge Mat', 'Standard', NULL),
    ('BK-2024-002', 'Vélo Ville Confort', '2024-04-02', 'Eurocycles', NULL, 15.200, 17.100,
        'Citadine Comfort', '16', '24', NULL, 'Bleu Océan', 'Polyester', NULL),
    ('BK-2024-003', 'BMX Freestyle Junior', '2024-05-20', 'Eurocycles Kids', '9876543210987', 8.300, 9.900,
        'Freestyle Jr', '14', '20', 'CUST-045', 'Jaune Fluo', 'Water Transfer', NULL),
    ('BK-2024-004', 'Vélo Route Performance', '2024-06-10', 'Eurocycles Sport', NULL, 9.100, 10.500,
        'Route Perf S2', '15', '29', 'CUST-102', 'Noir Carbone', 'Standard', NULL);
GO

INSERT INTO LigneNomenclature
    (Code, NomenclatureCode, Designation, Quantite, Prix, Fabricant, Imprime, Observation, Devise)
VALUES
    ('BK-2024-001-L1', 'BK-2024-001', 'Cadre aluminium 6061', 1.00, 145.500, 'AlloyTech', 1, NULL, 'Euro'),
    ('BK-2024-001-L2', 'BK-2024-001', 'Fourche suspension 120mm', 1.00, 89.990, 'SuspenPro', 0, 'Réglage usine', 'Euro'),
    ('BK-2024-001-L3', 'BK-2024-001', 'Groupe de transmission 21V', 1.00, 65.000, 'ShiftMaster', 1, NULL, 'USD'),

    ('BK-2024-002-L1', 'BK-2024-002', 'Panier avant osier', 1.00, 18.500, NULL, 0, NULL, 'Euro'),
    ('BK-2024-002-L2', 'BK-2024-002', 'Selle confort gel', 1.00, 22.750, 'ComfortSeat', 1, NULL, 'TND'),

    ('BK-2024-003-L1', 'BK-2024-003', 'Guidon renforcé', 1.00, 12.300, 'ProBar', 0, 'Anti-rouille', 'Euro'),
    ('BK-2024-003-L2', 'BK-2024-003', 'Pneus grip renforcé', 2.00, 15.000, 'GripTech', 1, NULL, 'Euro'),
    ('BK-2024-003-L3', 'BK-2024-003', 'Pédales anti-dérapantes', 1.00, 8.990, NULL, 0, NULL, 'YEN'),

    ('BK-2024-004-L1', 'BK-2024-004', 'Cadre carbone T800', 1.00, 320.000, 'CarbonWorks', 1, 'Pièce premium', 'Euro');
GO
