CREATE PROCEDURE [dbo].[spOrdersGetClientId]
	@ClientId int
AS
BEGIN
	SELECT [ProductId], [ProductQuantity], [ClientId] FROM dbo.Orders WHERE ClientId = @ClientId AND IsDone = 0;
END
