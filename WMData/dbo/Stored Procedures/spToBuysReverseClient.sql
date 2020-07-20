CREATE PROCEDURE [dbo].[spToBuysReverseClient]
	@ClientId int,
	@ProductId int,
	@ProductQuantity int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.Clients SET HasToBuy = 0 WHERE Id = @ClientId;

	DELETE FROM dbo.ToBuysProducts WHERE ProductId = @ProductId;
	
	DELETE FROM dbo.ToBuys WHERE ClientId = @ClientId
END