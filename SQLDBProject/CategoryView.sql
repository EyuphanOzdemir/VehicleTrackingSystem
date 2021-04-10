CREATE VIEW [dbo].[CategoryView]
AS
SELECT        Id, Name, MinWeight, dbo.FindMaxWeight(MinWeight) AS UpTo, IconFileName
FROM            dbo.Categories
