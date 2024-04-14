-- Create a new database called 'MediClubDb'
USE master
GO
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'MediClubDb'
)
CREATE DATABASE MediClubDb
GO

USE MediClubDb
GO

CREATE TABLE [dbo].[Doctors] (   
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Surname] nvarchar(50) NOT NULL,
    [BirthDate] datetime NOT NULL,
    [Phone] nvarchar(50) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
    [Specialization] nvarchar(100) NOT NULL
)

CREATE TABLE [dbo].[Patients] (   
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Surname] nvarchar(50) NOT NULL,
    [BirthDate] datetime NOT NULL,
    [Phone] nvarchar(50) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
    [Diagnosis] nvarchar(100) NOT NULL
)

INSERT INTO [dbo].[Doctors] VALUES ("John", "Smith", "1977-03-03", "+9940514556860", "jsmith@mail", "Cardiologist"),
                            ("Emma", "Wathson", "1967-01-03", "+9946324556860", "emmyWat@mail", "Dantist");
INSERT INTO [dbo].[Patients] VALUES ("Olga", "Petrova", "1981-13-08", "+99405174536860", "olgaPet@mail", "high blood pressure"),
                            ("Ali", "Aliyev", "1991-11-07", "+9940516436860", "alAli@mail", "high blood pressure")

