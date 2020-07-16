CREATE TABLE [dbo].[Clients]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PhoneNumber] NVARCHAR(128) NULL, 
    [Name] NVARCHAR(128) NULL, 
    [Surname] NVARCHAR(128) NULL, 
    [Adress] NVARCHAR(128) NULL, 
    [HasOrders] BIT NOT NULL DEFAULT 0, 
    [HasToBuy] BIT NOT NULL DEFAULT 0, 
    [HasHistory] NCHAR(10) NOT NULL DEFAULT 0, 

)
