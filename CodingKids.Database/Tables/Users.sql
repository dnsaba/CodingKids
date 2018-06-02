CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Salt] NVARCHAR(15) NULL, 
    [HashPassword] NVARCHAR(64) NULL, 
    [CreatedDate] DATETIME NOT NULL DEFAULT getutcdate(), 
    [ModifiedDate] DATETIME NOT NULL DEFAULT getutcdate(), 
    [Confirmed] BIT NULL, 
    [RoleId] INT NULL, 
    [ModifiedBy] NCHAR(10) NULL
)
