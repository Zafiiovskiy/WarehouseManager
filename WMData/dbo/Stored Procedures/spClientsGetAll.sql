CREATE PROCEDURE [dbo].[spClientsGetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [PhoneNumber], [Name], [Surname], [Adress] FROM dbo.Clients
END
