CREATE PROCEDURE [dbo].[spWareHouseGetById]
	@ProductId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [ProductId], [FactoryNumber], [Name], [Set], [Type], [Photo], [QuantityInStock], [ProductDescription], [NetPrice], [SellPrice] FROM dbo.WareHouse WHERE ProductId = @ProductId;
END