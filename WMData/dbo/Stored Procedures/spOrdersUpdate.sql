CREATE PROCEDURE [dbo].[spOrdersUpdate]
	@ProductId int,
	@OrderQuantity int,
	@WareHouseQuantity int,
	@ProductNetPrice money,
	@ProductSellPrice money,
	@ClientId int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Orders SET ProductQuantity = @OrderQuantity,
							ProductNetPrice = @ProductNetPrice,
							ProductSellPrice = @ProductSellPrice
							WHERE ProductId = @ProductId AND ClientId = @ClientId;

	UPDATE dbo.WareHouse SET QuantityInStock = @WareHouseQuantity,
							NetPrice = @ProductNetPrice,
							SellPrice = @ProductSellPrice
							WHERE ProductId = @ProductId;
END
