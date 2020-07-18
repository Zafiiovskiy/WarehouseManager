CREATE PROCEDURE [dbo].[spWareHousePostAll]
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
	INSERT INTO dbo.WareHouse(FactoryNumber,[Name],[Set],[Type],[Photo],[QuantityInStock],[ProductDescription],NetPrice,SellPrice) VALUES(@FactoryNumber,@Name,@Set,@Type,@Photo,@QuantityInStock,@ProductDescription,@NetPrice,@SellPrice);
END
