CREATE PROCEDURE [dbo].[spClientsGetHaveToBuys]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [PhoneNumber], [Name], [Surname], [Adress] FROM dbo.Clients WHERE HasToBuy = 1 ORDER BY [Name] ASC, Surname ASC; 
END
