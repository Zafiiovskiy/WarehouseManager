CREATE PROCEDURE [dbo].[spWareHouseUpdateAll]
	@ProductId int,
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
    UPDATE dbo.WareHouse
    SET FactoryNumber = @FactoryNumber,[Name] = @Name,[Set] = @Set,[Type] = @Type
    ,Photo = @Photo,QuantityInStock = @QuantityInStock,ProductDescription = @ProductDescription
    ,NetPrice=@NetPrice,SellPrice=@SellPrice
    WHERE ProductId = @ProductId
END
