CREATE TABLE [dbo].[ToBuys]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductId] INT NOT NULL, 
    [ProductQuantity] INT NOT NULL ,
    [ProductNetPrice] MONEY NOT NULL, 
    [ProductSellPrice] MONEY NOT NULL, 
    [ClientId] INT NOT NULL, 
    [OrderDateTime] DATETIME2 NOT NULL DEFAULT getutcdate() , 
    [IsDone] BIT NOT NULL DEFAULT 0

)
