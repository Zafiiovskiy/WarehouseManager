CREATE PROCEDURE [dbo].[spOrdersReverseClient]
	@ClientId int,
	@ProductId int,
	@ProductQuantity int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.Clients SET HasOrders = 0 WHERE Id = @ClientId;

	UPDATE dbo.WareHouse SET QuantityInStock+= @ProductQuantity,IsOrdered = 0 WHERE ProductId = @ProductId;
	
	DELETE FROM dbo.Orders WHERE ClientId = @ClientId
END

