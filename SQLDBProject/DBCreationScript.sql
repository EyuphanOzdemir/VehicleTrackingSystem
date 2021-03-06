USE [master]
GO
/****** Object:  Database [VehicleTrackingSystem]    Script Date: 12.04.2021 09:50:28 ******/
CREATE DATABASE [VehicleTrackingSystem]
 GO
ALTER DATABASE [VehicleTrackingSystem] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VehicleTrackingSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VehicleTrackingSystem] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [VehicleTrackingSystem] SET ANSI_NULLS ON 
GO
ALTER DATABASE [VehicleTrackingSystem] SET ANSI_PADDING ON 
GO
ALTER DATABASE [VehicleTrackingSystem] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [VehicleTrackingSystem] SET ARITHABORT ON 
GO
ALTER DATABASE [VehicleTrackingSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VehicleTrackingSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [VehicleTrackingSystem] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [VehicleTrackingSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [VehicleTrackingSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VehicleTrackingSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VehicleTrackingSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VehicleTrackingSystem] SET  MULTI_USER 
GO
ALTER DATABASE [VehicleTrackingSystem] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [VehicleTrackingSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VehicleTrackingSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VehicleTrackingSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [VehicleTrackingSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VehicleTrackingSystem] SET QUERY_STORE = OFF
GO
USE [VehicleTrackingSystem]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [VehicleTrackingSystem]
GO
/****** Object:  UserDefinedFunction [dbo].[FindCategoryByWeight]    Script Date: 12.04.2021 09:50:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FindCategoryByWeight]
(
	@weight decimal(9,2)
)
RETURNS int
begin
	declare @result int=-1
	select @result=max(Id) from Categories where MinWeight<=@weight
	return @result
end
GO
/****** Object:  UserDefinedFunction [dbo].[FindMaxWeight]    Script Date: 12.04.2021 09:50:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FindMaxWeight]
(
	@minWeight decimal(9,2)
)
RETURNS decimal(9,2)
begin
	declare @result decimal(9,2)
	select @result=min(MinWeight) from Categories where MinWeight>@minWeight
	if @result is null set @result=-1
	return @result
end
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12.04.2021 09:50:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[MinWeight] [decimal](9, 2) NOT NULL,
	[IconFileName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[CategoryView]    Script Date: 12.04.2021 09:50:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CategoryView]
AS
SELECT        Id, Name, MinWeight, dbo.FindMaxWeight(MinWeight) AS UpTo, IconFileName
FROM            dbo.Categories
GO
/****** Object:  Table [dbo].[Manifacturers]    Script Date: 12.04.2021 09:50:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manifacturers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Manifacturers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 12.04.2021 09:50:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OwnerName] [nvarchar](200) NOT NULL,
	[ManifacturerId] [int] NOT NULL,
	[YearOfManifacture] [int] NOT NULL,
	[Weight] [decimal](9, 2) NOT NULL,
	[CategoryId]  AS ([dbo].[FindCategoryByWeight]([Weight])),
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VehicleView]    Script Date: 12.04.2021 09:50:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VehicleView]
	AS SELECT V.Id, OwnerName, M.Id as ManifacturerId, M.Name as Manifacturer, V.YearOfManifacture, V.Weight, C.Id as CategoryId, C.Name as CategoryName, C.IconFileName as IconFileName
	FROM Vehicles V inner join Categories C on V.CategoryId=C.Id 
	inner join Manifacturers M on V.ManifacturerId=M.Id
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [MinWeight], [IconFileName]) VALUES (1, N'Light', CAST(0.00 AS Decimal(9, 2)), N'light.png')
INSERT [dbo].[Categories] ([Id], [Name], [MinWeight], [IconFileName]) VALUES (2, N'Medium', CAST(500.00 AS Decimal(9, 2)), N'medium.png')
INSERT [dbo].[Categories] ([Id], [Name], [MinWeight], [IconFileName]) VALUES (3, N'Heavy', CAST(2500.00 AS Decimal(9, 2)), N'heavy.png')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Manifacturers] ON 

INSERT [dbo].[Manifacturers] ([Id], [Name]) VALUES (4, N'Ferrari')
INSERT [dbo].[Manifacturers] ([Id], [Name]) VALUES (3, N'Honda')
INSERT [dbo].[Manifacturers] ([Id], [Name]) VALUES (1, N'Mazda')
INSERT [dbo].[Manifacturers] ([Id], [Name]) VALUES (2, N'Mercedes')
INSERT [dbo].[Manifacturers] ([Id], [Name]) VALUES (5, N'Toyota')
SET IDENTITY_INSERT [dbo].[Manifacturers] OFF
GO
/****** Object:  Index [IX_Categories_MinWeight]    Script Date: 12.04.2021 09:50:28 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Categories_MinWeight] ON [dbo].[Categories]
(
	[MinWeight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Categories_Name]    Script Date: 12.04.2021 09:50:28 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Categories_Name] ON [dbo].[Categories]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Manifacturers_Name]    Script Date: 12.04.2021 09:50:28 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Manifacturers_Name] ON [dbo].[Manifacturers]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicles_Manifacturer]    Script Date: 12.04.2021 09:50:28 ******/
CREATE NONCLUSTERED INDEX [IX_Vehicles_Manifacturer] ON [dbo].[Vehicles]
(
	[ManifacturerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Vehicles_OwnerName]    Script Date: 12.04.2021 09:50:28 ******/
CREATE NONCLUSTERED INDEX [IX_Vehicles_OwnerName] ON [dbo].[Vehicles]
(
	[OwnerName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Vehicles]  WITH NOCHECK ADD  CONSTRAINT [FK_Vehicles_ToManifacturer] FOREIGN KEY([ManifacturerId])
REFERENCES [dbo].[Manifacturers] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_ToManifacturer]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [CK_Categories_MinWeight] CHECK  (([MinWeight]>=(0)))
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [CK_Categories_MinWeight]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [CK_Vehicles_Weight] CHECK  (([Weight]>(0)))
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [CK_Vehicles_Weight]
GO
USE [master]
GO
ALTER DATABASE [VehicleTrackingSystem] SET  READ_WRITE 
GO
