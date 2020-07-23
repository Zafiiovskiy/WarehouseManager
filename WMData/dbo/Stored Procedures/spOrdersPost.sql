CREATE PROCEDURE [dbo].[spOrdersPost]
	@ProductId int,
	@ProductQuantity int,
	@ProductNetPrice money,
	@ProductSellPrice money, 
    @ClientId int 
AS
BEGIN
	SET NOCOUNT ON;
	IF (SELECT COUNT(*) FROM dbo.Orders WHERE ProductId = @ProductId AND ClientId = @ClientId) = 0
		BEGIN
		INSERT INTO dbo.Orders(ProductId,ProductQuantity,ProductNetPrice,ProductSellPrice,ClientId) VALUES(@ProductId,@ProductQuantity,@ProductNetPrice,@ProductSellPrice,@ClientId);
	
		UPDATE dbo.WareHouse
		SET QuantityInStock -= @ProductQuantity,
		IsOrdered = 1
		WHERE ProductId = @ProductId;

		UPDATE dbo.Clients
		SET HasOrders = 1
		WHERE Id = @ClientId;
		END
	ELSE
		BEGIN
		UPDATE dbo.Orders SET ProductQuantity += @ProductQuantity WHERE ProductId = @ProductId AND ClientId = @ClientId;

		UPDATE dbo.WareHouse
		SET QuantityInStock -= @ProductQuantity
		WHERE ProductId = @ProductId;
		END
END
