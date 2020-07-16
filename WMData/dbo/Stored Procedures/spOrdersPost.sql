CREATE PROCEDURE [dbo].[spOrdersPost]
	@ProductId int,
	@ProductQuantity int,
	@ProductNetPrice money,
	@ProductSellPrice money, 
    @ClientId int 
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.Orders(ProductId,ProductQuantity,ProductNetPrice,ProductSellPrice,ClientId) VALUES(@ProductId,@ProductQuantity,@ProductNetPrice,@ProductSellPrice,@ClientId);
	
	UPDATE dbo.WareHouse
    SET QuantityInStock -= @ProductQuantity,
	IsOrdered = 1
    WHERE ProductId = @ProductId

	UPDATE dbo.Clients
	SET HasOrders = 1
	WHERE Id = @ClientId
END
