CREATE TABLE [dbo].[Manifacturers]
(
	[Id] INT NOT NULL IDENTITY (1,1), 
    [Name] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_Manifacturers] PRIMARY KEY (Id)
)

GO


CREATE UNIQUE INDEX [IX_Manifacturers_Name] ON [dbo].[Manifacturers] ([Name])
