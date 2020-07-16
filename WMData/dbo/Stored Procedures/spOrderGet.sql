CREATE PROCEDURE [dbo].[spOrderGet]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [ProductId], [ProductQuantity], [ProductNetPrice], [ProductSellPrice], [ClientId], [OrderDateTime], [IsDone] FROM dbo.Orders WHERE IsDone = 0
END
