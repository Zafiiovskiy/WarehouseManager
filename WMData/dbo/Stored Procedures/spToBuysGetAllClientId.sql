CREATE PROCEDURE [dbo].[spToBuysGetAllClientId]
	@ClientId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [ProductId], [ProductQuantity], [ProductNetPrice], [ProductSellPrice], [ClientId], [OrderDateTime] FROM dbo.ToBuys WHERE ClientId = @ClientId;
END
