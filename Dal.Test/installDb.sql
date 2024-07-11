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