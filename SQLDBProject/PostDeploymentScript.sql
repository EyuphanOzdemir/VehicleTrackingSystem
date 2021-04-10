/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
--The initial manifacturers
truncate table Vehicles
truncate table Manifacturers
truncate table Categories

Insert into Manifacturers Values ('Mazda'), ('Mercedes'), ('Honda'), ('Ferrari'), ('Toyota')

--The default categories
Insert into Categories Values ('Light', 0, 'light.png'), ('Medium', 500, 'medium.png'), ('Heavy', 2500, 'heavy.png')


ALTER TABLE [dbo].[Vehicles] WITH NOCHECK
    ADD CONSTRAINT [FK_Vehicles_ToManifacturer] FOREIGN KEY ([ManifacturerId]) REFERENCES [dbo].[Manifacturers] ([Id]);

ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_ToManifacturer]

