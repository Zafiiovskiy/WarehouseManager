CREATE PROCEDURE [dbo].[spToBuysPost]
	@ProductId int,
	@ProductQuantity int,
	@ProductNetPrice money,
	@ProductSellPrice money, 
    @ClientId int 
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.ToBuys(ProductId,ProductQuantity,ProductNetPrice,ProductSellPrice,ClientId) VALUES(@ProductId,@ProductQuantity,@ProductNetPrice,@ProductSellPrice,@ClientId);

	UPDATE dbo.Clients
	SET HasToBuy = 1
	WHERE Id = @ClientId;
END
