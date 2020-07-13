CREATE TABLE [dbo].[Clients]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PhoneNumber] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(128) NULL, 
    [Surname] NVARCHAR(128) NULL, 
    [Adress] NVARCHAR(128) NULL, 

)
