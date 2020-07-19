CREATE PROCEDURE [dbo].[spOrdersReversByProductId]
	@ClientId int,
	@ProductId int,
	@ProductQuantity int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.WareHouse SET QuantityInStock+= @ProductQuantity,IsOrdered = 0 WHERE ProductId = @ProductId;
	
	DELETE FROM dbo.Orders WHERE ProductId = @ProductId

	IF (SELECT COUNT(*) FROM dbo.Orders WHERE ClientId = @ClientId) = 0
		BEGIN
			UPDATE dbo.Clients SET HasOrders = 0 WHERE Id = @ClientId;
		END
END

