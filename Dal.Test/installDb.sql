CREATE DATABASE implantdentTest
GO

USE implantdentTest
GO

CREATE TABLE [User] (
    [UserId] SMALLINT      PRIMARY KEY IDENTITY,
    [Email]  VARCHAR (200) NOT NULL,
    [Name]   VARCHAR (400) NOT NULL, 
    [Password] VARCHAR(128) NOT NULL, 
    [Active] BIT NOT NULL
)
GO

INSERT INTO [User] ([Email], [Name], [Password], [Active]) VALUES ('test1@test.com', 'Test 1', 'Pass1', 1),('test2@test.com', 'Test 2', 'Pass2', 1),('test3@test.com', 'Test 3', 'Pass3', 1)
GO