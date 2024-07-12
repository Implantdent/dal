CREATE DATABASE implantdentTest
GO

USE implantdentTest
GO

CREATE TABLE [User] (
    [UserId] SMALLINT      PRIMARY KEY IDENTITY,
    [Email]  VARCHAR (50) NOT NULL,
    [Name]   VARCHAR (100) NOT NULL, 
    [Password] VARCHAR(128) NOT NULL, 
    [Active] BIT NOT NULL
)
GO

CREATE UNIQUE INDEX [UK_User_Email] ON [dbo].[User] ([Email])
GO

INSERT INTO [User] ([Email], [Name], [Password], [Active])
VALUES
('test1@test.com', 'Test 1', CONVERT(VARCHAR(MAX), HASHBYTES('SHA2_512', 'Pass1'), 2), 1),
('test2@test.com', 'Test 2', CONVERT(VARCHAR(MAX), HASHBYTES('SHA2_512', 'Pass2'), 2), 1),
('test3@test.com', 'Test 3', CONVERT(VARCHAR(MAX), HASHBYTES('SHA2_512', 'Pass3'), 2), 1)
GO

CREATE TABLE [Role]
(
	[RoleId] SMALLINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL
)
GO

INSERT INTO [Role] ([Name])
VALUES
('Test 1'),
('Test 2'),
('Test 3')
GO

CREATE TABLE [UserRole]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [UserId] SMALLINT NOT NULL,
    [RoleId] SMALLINT NOT NULL,
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([RoleId])
)

GO

CREATE UNIQUE INDEX [UK_UserRole] ON [dbo].[UserRole] ([UserId], [RoleId])
GO

INSERT INTO [UserRole] ([UserId], [RoleId])
VALUES
(1, 1),
(1, 2),
(2, 1),
(2, 2)
GO

CREATE VIEW [VwUserRole] AS
SELECT
	u.UserId, u.Email, u.Name AS [User], u.Active, r.RoleId, r.Name AS [Role]
FROM
	[User] u
	INNER JOIN [UserRole] ur
		ON u.UserId = ur.UserId
	INNER JOIN [Role] r
		ON ur.RoleId = r.RoleId
GO

CREATE TABLE [LogDb]
(
	[LogDbId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATETIME NOT NULL, 
    [Action] CHAR NOT NULL, 
    [TableId] BIGINT NOT NULL, 
    [Table] VARCHAR(200) NOT NULL, 
    [Values] TEXT NOT NULL, 
    [UserId] SMALLINT NOT NULL, 
    CONSTRAINT [FK_LogDb_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
GO

INSERT INTO [LogDb] ([Date], [Action], [TableId], [Table], [Values], [UserId])
VALUES
(GETDATE(), 'I', 1, 'LogDb', 'Test 1', 1),
(GETDATE(), 'U', 2, 'LogDb', 'Test 2', 1),
(GETDATE(), 'D', 3, 'LogDb', 'Test 3', 1)
GO

CREATE VIEW [VwLogDb] AS
SELECT
	l.LogDbId, l.[Date], l.[Action], l.TableId, l.[Table], l.[Values], l.UserId, u.Email, u.Name, u.Active
FROM
	[LogDb] l
	INNER JOIN [User] u
		ON l.UserId = u.UserId
GO