CREATE TABLE [dbo].[History]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FactoryNumber] NVARCHAR(10) NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL, 
    [Set] NCHAR(200) NOT NULL , 
    [Type] NCHAR(128) NOT NULL, 
    [Photo] IMAGE NULL,
    [ProductDescription] NVARCHAR(MAX) NULL,  
    [ProductQuantity] INT NOT NULL ,
    [ProductNetPrice] MONEY NOT NULL, 
    [ProductSellPrice] MONEY NOT NULL, 
    [ClientId] INT NOT NULL, 
    [OrderDateTime] DATETIME2 NOT NULL,
    [FinishDateTime] DATETIME2 NOT NULL DEFAULT getutcdate(),
)
