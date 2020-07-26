CREATE PROCEDURE [dbo].[spOrdersGetAllByClientId]
	@ClientId int
AS
BEGIN
	SELECT [ProductId], [ProductQuantity], [ProductNetPrice], [ProductSellPrice], [ClientId], [OrderDateTime] FROM dbo.Orders WHERE ClientId = @ClientId;
END