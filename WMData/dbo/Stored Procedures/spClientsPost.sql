CREATE PROCEDURE [dbo].[spClientsPost]
	@PhoneNumber nvarchar(128),
	@Name nvarchar(128),
	@Surname nvarchar(128),
	@Adress nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.Clients VALUES(@PhoneNumber,@Name,@Surname,@Adress);
END
