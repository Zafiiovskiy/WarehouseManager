CREATE TABLE [dbo].[WareHouse]
(
	[ProductId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FactoryNumber] NVARCHAR(10) NULL, 
    [Name] NVARCHAR(128) NULL, 
    [Set] NCHAR(200) NULL , 
    [Type] NCHAR(128) NULL, 
    [Photo] IMAGE NULL, 
    [QuantityInStock] INT NOT NULL DEFAULT 1, 
    [ProductDescription] NVARCHAR(MAX) NULL, 
    [NetPrice] MONEY NOT NULL DEFAULT 0, 
    [SellPrice] MONEY NOT NULL DEFAULT 0, 
    [IsOrdered] BIT NOT NULL DEFAULT 0  
)
