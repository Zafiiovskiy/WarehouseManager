CREATE PROCEDURE [dbo].[spToBuysProductsPostAll]
	@FactoryNumber nvarchar(128),
	@Name nvarchar(128),
	@Set NCHAR(128),
	@Type NCHAR(128), 
    @Photo IMAGE, 
    @QuantityInStock INT, 
    @ProductDescription NVARCHAR(MAX), 
    @NetPrice MONEY, 
    @SellPrice MONEY  
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.ToBuysProducts(FactoryNumber,[Name],[Set],[Type],[Photo],[QuantityInStock],[ProductDescription],NetPrice,SellPrice) VALUES(@FactoryNumber,@Name,@Set,@Type,@Photo,@QuantityInStock,@ProductDescription,@NetPrice,@SellPrice);
	
	SELECT [ProductId], [FactoryNumber], [Name], [Set], [Type], [Photo], [QuantityInStock], [ProductDescription], [NetPrice], [SellPrice] FROM dbo.ToBuysProducts WHERE FactoryNumber = @FactoryNumber AND [Name] = @Name AND NetPrice = @NetPrice;

END

