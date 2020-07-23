CREATE PROCEDURE [dbo].[spToBuysReversByProductId]
	@ClientId int,
	@ProductId int,
	@ProductQuantity int
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE FROM dbo.ToBuysProducts WHERE ProductId = @ProductId;
	
	DELETE FROM dbo.ToBuys WHERE ProductId = @ProductId AND ClientId = @ClientId AND IsDone = 0

	IF (SELECT COUNT(*) FROM dbo.ToBuys WHERE ClientId = @ClientId) = 0
		BEGIN
			UPDATE dbo.Clients SET HasToBuy = 0 WHERE Id = @ClientId;
		END
END
