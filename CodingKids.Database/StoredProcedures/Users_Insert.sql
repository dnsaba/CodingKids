CREATE PROCEDURE [dbo].[Users_Insert]
	@Id int OUT,
	@UserName nvarchar(50),
	@Salt nvarchar(15),
	@HashPassword nvarchar(64)
AS
/*
DECLARE 
	@_id int, 
	@_email nvarchar(50) = 'dj@mailinator.com',
	@_salt nvarchar(15) = '209344312312334',
	@_hashPassword nvarchar(64) = 'lsknjfas;kdjn13213213213fhhehfiosnfsadfdsfa';

EXECUTE Users_Insert
	@_id OUT,
	@_email,
	@_salt,
	@_hashPassword;

SELECT @_id;
SELECT * FROM Users WHERE Id = @_id;
*/
BEGIN
	INSERT INTO Users (
		UserName,
		Salt,
		HashPassword)
	VALUES (
		@UserName,
		@Salt,
		@HashPassword);

	SET @Id = SCOPE_IDENTITY();
END



