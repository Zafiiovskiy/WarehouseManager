CREATE PROCEDURE [dbo].[spToBuysProductsGetById]
	@ProductId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [ProductId], [FactoryNumber], [Name], [Set], [Type], [Photo], [QuantityInStock], [ProductDescription], [NetPrice], [SellPrice] FROM dbo.ToBuysProducts WHERE ProductId = @ProductId;
END
