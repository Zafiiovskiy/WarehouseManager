CREATE PROCEDURE [dbo].[spClientsGetHaveOrders]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [PhoneNumber], [Name], [Surname], [Adress] FROM dbo.Clients WHERE HasOrders = 1;
END
