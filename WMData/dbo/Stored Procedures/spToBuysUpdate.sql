CREATE PROCEDURE [dbo].[spToBuysUpdate]
	@ProductId int,
	@ProductQuantity int,
	@ProductNetPrice money,
	@ProductSellPrice money,
	@ClientId int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.ToBuys SET ProductQuantity = @ProductQuantity,
							ProductNetPrice = @ProductNetPrice,
							ProductSellPrice = @ProductSellPrice
							WHERE ProductId = @ProductId AND ClientId = @ClientId;

	UPDATE dbo.ToBuysProducts SET QuantityInStock = @ProductQuantity,
							NetPrice = @ProductNetPrice,
							SellPrice = @ProductSellPrice
							WHERE ProductId = @ProductId;
END
