CREATE PROCEDURE [dbo].[spWareHousePostAll]
	@FactoryNumber nvarchar(10),
	@Name nvarchar(128),
	@Set NCHAR(200),
	@Type NCHAR(128), 
    @Photo IMAGE, 
    @QuantityInStock INT, 
    @ProductDescription NVARCHAR(MAX), 
    @NetPrice MONEY, 
    @SellPrice MONEY  
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.WareHouse VALUES(@FactoryNumber,@Name,@Set,@Type,@Photo,@QuantityInStock,@ProductDescription,@NetPrice,@SellPrice);
END
