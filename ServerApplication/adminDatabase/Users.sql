CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(20) NULL, 
    [Password] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(MAX) NULL, 
    [Role] INT NULL, 
    [Salt] NVARCHAR(50) NULL, 
    [SaltedPassword] NVARCHAR(50) NULL
)
