CREATE PROCEDURE [dbo].[spClientsGetById]
	@ClientId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [PhoneNumber], [Name], [Surname], [Adress] FROM dbo.Clients WHERE Id = @ClientId;
END
