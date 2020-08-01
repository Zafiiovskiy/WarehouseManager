CREATE PROCEDURE [dbo].[spHistoryGetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [FactoryNumber], [Name], [Set], [Type],
			[Photo], [ProductDescription], [ProductQuantity], [ProductNetPrice], 
			[ProductSellPrice], [ClientId], [OrderDateTime], [FinishDateTime] FROM dbo.History;
END
