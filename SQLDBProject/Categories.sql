CREATE TABLE [dbo].[Categories]
(
	[Id] INT NOT NULL IDENTITY (1,1), 
	[Name] NVARCHAR(100) NOT NULL, 
	[MinWeight] decimal(9,2) NOT NULL, 
	[IconFileName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY (Id), 
    CONSTRAINT [CK_Categories_MinWeight] CHECK (MinWeight >=0) 
)

GO

CREATE UNIQUE INDEX [IX_Categories_MinWeight] ON [dbo].[Categories] ([MinWeight])

GO

CREATE UNIQUE INDEX [IX_Categories_Name] ON [dbo].[Categories] ([Name])
