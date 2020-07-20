CREATE PROCEDURE [dbo].[spToBuysGetClientId]
	@ClientId int
AS
BEGIN
	SELECT [ProductId], [ProductQuantity], [ClientId] FROM dbo.ToBuys WHERE ClientId = @ClientId AND IsDone = 0;
END
