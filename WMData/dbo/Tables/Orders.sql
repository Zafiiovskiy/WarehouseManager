CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductId] INT NOT NULL, 
    [ProductQuantity] INT NOT NULL ,
    [ProductNetPrice] MONEY NOT NULL, 
    [ProductSellPrice] MONEY NOT NULL, 
    [ClientId] INT NOT NULL, 
    [OrderDateTime] DATETIME2 NOT NULL DEFAULT getutcdate() , 
    [IsDone] BIT NOT NULL DEFAULT 0

    CONSTRAINT [FK_Orders_ToTable_WareHouse] FOREIGN KEY ([ProductId]) REFERENCES dbo.Warehouse([ProductId]), 
    CONSTRAINT [FK_Orders_ToTable_Clients] FOREIGN KEY ([ClientId]) REFERENCES dbo.Clients([Id]),

)
