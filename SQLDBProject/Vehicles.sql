CREATE TABLE [dbo].[Vehicles]
(
	[Id] INT NOT NULL IDENTITY (1,1), 
	[OwnerName] NVARCHAR(200) NOT NULL, 
	[ManifacturerId] INT NOT NULL, 
    [YearOfManifacture] INT NOT NULL, 
    [Weight] DECIMAL(9, 2) NOT NULL, 
    [CategoryId] as dbo.FindCategoryByWeight(Weight),
    CONSTRAINT [PK_Vehicles] PRIMARY KEY (Id), 
    CONSTRAINT [CK_Vehicles_Weight] CHECK (Weight>0),
)

GO

CREATE INDEX [IX_Vehicles_OwnerName] ON [dbo].[Vehicles] ([OwnerName])
GO
CREATE INDEX [IX_Vehicles_Manifacturer] ON [dbo].[Vehicles] ([ManifacturerId])
