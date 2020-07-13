CREATE PROCEDURE [dbo].[spClientsUpdate]
	@Id int,
    @PhoneNumber nvarchar(50),
	@Name nvarchar(128),
	@Surname nvarchar(128),
	@Adress nvarchar(128) 
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Clients
    SET PhoneNumber = @PhoneNumber, [Name] = @Name, Surname = @Surname,Adress = @Adress
    WHERE Id = @Id
END
