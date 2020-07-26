CREATE PROCEDURE [dbo].[spOrdersGetClientId]
	@ClientId int
AS
BEGIN
	SELECT [ProductId], [ProductQuantity], [ClientId] FROM dbo.Orders WHERE ClientId = @ClientId;
END
