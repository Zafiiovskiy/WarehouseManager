CREATE PROCEDURE [dbo].[spWareHouseGetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [ProductId], [FactoryNumber], [Name], [Set], [Type], [Photo], [QuantityInStock], [ProductDescription], [NetPrice], [SellPrice] from dbo.WareHouse WHERE isToBuy = 0;
END
