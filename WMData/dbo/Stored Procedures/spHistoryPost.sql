CREATE PROCEDURE [dbo].[spHistoryPost]
    @Sender bit,
    @ProductId int,
	@FactoryNumber NVARCHAR(10), 
    @Name NVARCHAR(128), 
    @Set NCHAR(200), 
    @Type NCHAR(128), 
    @Photo IMAGE,
    @ProductDescription NVARCHAR(MAX),  
    @ProductQuantity INT,
    @ProductNetPrice MONEY, 
    @ProductSellPrice MONEY, 
    @ClientId INT, 
    @OrderDateTime DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    IF @Sender = 1
        BEGIN
	    INSERT INTO dbo.History(FactoryNumber, [Name], [Set], [Type], Photo,ProductDescription,  ProductQuantity, ProductNetPrice, ProductSellPrice, ClientId, OrderDateTime)
            Values(@FactoryNumber,  @Name, @Set,  @Type,  @Photo,@ProductDescription, @ProductQuantity, @ProductNetPrice,  @ProductSellPrice, @ClientId, @OrderDateTime)
        UPDATE dbo.Orders SET IsDone = 0 WHERE ClientId = @ClientId AND ProductId = @ProductId;
        UPDATE dbo.Clients SET HasHistory = 1, HasOrders = 0 WHERE Id = @ClientId;        DELETE FROM dbo.Orders WHERE ClientId = @ClientId;
        END
    ELSE IF @Sender = 0
        BEGIN
             INSERT INTO dbo.History(FactoryNumber, [Name], [Set], [Type], Photo,ProductDescription,  ProductQuantity, ProductNetPrice, ProductSellPrice, ClientId, OrderDateTime)
            Values(@FactoryNumber,  @Name, @Set,  @Type,  @Photo,@ProductDescription, @ProductQuantity, @ProductNetPrice,  @ProductSellPrice, @ClientId, @OrderDateTime)
        UPDATE dbo.ToBuys SET IsDone = 0 WHERE ClientId = @ClientId AND ProductId = @ProductId
        UPDATE dbo.Clients SET HasHistory = 1, HasToBuy = 0 WHERE Id = @ClientId;

        DELETE FROM dbo.ToBuys WHERE ClientId = @ClientId AND ProductId = @ProductId;
        DELETE FROM dbo.ToBuysProducts WHERE ProductId = @ProductId;
        END
END
