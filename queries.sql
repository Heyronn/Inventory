--Queries plant
INSERT INTO Plant (Plant_description, Active)
VALUES ('A1', '1'), ('A2', '1'), ('PWT', '1'), ('C1', '1'), ('C2', '1'), ('NMEX', '1')

INSERT INTO Plant (Plant_description, Active)
VALUES ('EJEMPLO1', '1'), ('EJEMPLO2', '1')

DELETE Plant
WHERE Plant_id = 8;

SELECT*
FROM Plant


SELECT Plant_id, Plant_description, Active
FROM Plant
WHERE Plant_id = 1009

SELECT Plant_id
FROM Plant
WHERE Plant_description = 'A1'




--Queries Location
INSERT INTO Location(Location_description, Plant_id, Active)
VALUES ('Trim B&K', 1, 1), ('Estampado rollos', 1, 1), ('Pintura', 2, 1)

SELECT Location_id, Location_description, L.Plant_id, L.Active
FROM Location AS L inner join Plant AS P 
ON L.Plant_id = P.Plant_id 
WHERE L.Active=1

SELECT Location_id, Location_description, L.Plant_id, L.Active, P.Plant_description AS Plant
FROM Location AS L inner join Plant AS P 
ON L.Plant_id = P.Plant_id 
WHERE L.Active=1 and Location_id = 4 and L.Plant_id = 1

SELECT*
FROM Location


UPDATE Location
SET Location_description= 'Ensamble YD XD', Plant_id='3',Active='1'
WHERE Location_id = 21


DELETE Location
WHERE Location_id <11 and Location_id >6


--Queries Models
SELECT Model_id, Model_description, Active
FROM Model
Where Active = 1



SELECT Model_id, Model_description, Active
FROM Model
Where Active = 1


INSERT INTO Model (Model_description, Active)
VALUES ('HP PROBOOK', 1)

UPDATE Model
SET Model_description = 'Prueba', Active = '1'
WHERE Model_id = 1;

DELETE Model
--Queries Versions

SELECT Version_id, Version_description,Active, EndOfSupport
FROM Version
WHERE Active = 1;

SELECT Version_id, Version_description,Active, EndOfSupport
FROM Version
WHERE Version_id = 1;

INSERT INTO Version (Version_description, Active, EndOfSupport)
VALUES ('Prueba', 1, '2024-02-13')

--UPDATE Version
--SET Version_description = @Version_description,EndOfSupport = @EndOfSupport, Active = '1'
--WHERE Version_id = @Version_id;

Delete Version
--Queries DivicesTypes

SELECT*
FROM DeviceType

SELECT D_type_id, D_type_description, Active
FROM DeviceType
WHERE Active = 1;

INSERT INTO DeviceType (D_type_description, Active)
VALUES ('Prueba Divece Type', 1)

UPDATE DeviceType
SET D_type_description = 'Editado', Active = '1'
WHERE D_type_id = 0;

DELETE DeviceType
--Queries Inventory

SELECT I.Inventory_id,I.D_type_id, I.Location_id, I.Version_id, I.Model_id, DT.D_type_description AS DeviceType, I.SerialNo, I.PurchaseDate, L.Location_description AS Location, V.Version_description AS Version, M.Model_description AS Model, I.Active
FROM Inventory AS I inner join DeviceType AS DT 
ON I.D_type_id = DT.D_type_id inner join Version AS V
ON V.Version_id = I.Version_id inner join Model AS M
ON M.Model_id = I.Model_id inner join Location AS L
ON L.Location_id = I.Location_id
WHERE I.Inventory_id = 1010



INSERT INTO Inventory (D_type_id, SerialNo, PurchaseDate, Location_id, Version_id, Model_id, Active)
VALUES (1,'2432', '2024-02-13', 1, 1, 1, '1')
SELECT SCOPE_IDENTITY();


SELECT*
FROM Inventory

Delete Inventory

--UPDATE Inventory
--SET D_type_id = @D_type_id, SerialNo = @SerialNo, PurchaseDate = @PurchaseDate, Location_id = @Location_id, Version_id = @Version_id, Model_id = @Model_id, Active = '1'
--WHERE Inventory_id = @Inventory_id;



SELECT I.Inventory_id,I.D_type_id, I.Location_id, I.Version_id, I.Model_id, DT.D_type_description AS DeviceType, I.SerialNo, I.PurchaseDate, L.Location_description AS Location, V.Version_description AS Version, M.Model_description AS Model, I.Active
FROM Inventory AS I inner join DeviceType AS DT 
ON I.D_type_id = DT.D_type_id inner join Version AS V
ON V.Version_id = I.Version_id inner join Model AS M
ON M.Model_id = I.Model_id inner join Location AS L
ON L.Location_id = I.Location_id
WHERE I.Active=1 and L.Plant_id = 1009;


SELECT I.Inventory_id, I.Plant, L.Location_id
FROM Inventory AS I inner join Location AS L 
ON I.Location_id = L.Location_id
WHERE L.Location_id = 1025





