CREATE VIEW [dbo].[VehicleView]
	AS SELECT V.Id, OwnerName, M.Id as ManifacturerId, M.Name as Manifacturer, V.YearOfManifacture, V.Weight, C.Id as CategoryId, C.Name as CategoryName, C.IconFileName as IconFileName
	FROM Vehicles V inner join Categories C on V.CategoryId=C.Id 
	inner join Manifacturers M on V.ManifacturerId=M.Id 
