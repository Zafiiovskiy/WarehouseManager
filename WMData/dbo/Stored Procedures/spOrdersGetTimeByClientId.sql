CREATE PROCEDURE [dbo].[spOrdersGetTimeByClientId]
	@ClientId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT OrderDateTime FROM dbo.Orders WHERE ClientId = @ClientId;
END

