CREATE DATABASE MediClubDb;

USE MediClubDb;

CREATE TABLE Doctors (
  Id INT PRIMARY KEY IDENTITY,
  FirstName NVARCHAR(100) NOT NULL,
  LastName NVARCHAR(100) NOT NULL,
  DateOfBirth DATE NOT NULL,
  Gender NVARCHAR(10) NOT NULL,
  Email NVARCHAR(100),
  PhoneNumber NVARCHAR(20),
  Specialization NVARCHAR(50),
  Department NVARCHAR(50)
);

CREATE TABLE Patients (
  Id INT PRIMARY KEY IDENTITY,
  FirstName NVARCHAR(100) NOT NULL,
  LastName NVARCHAR(100) NOT NULL,
  DateOfBirth DATE NOT NULL,
  Gender NVARCHAR(10) NOT NULL,
  Address NVARCHAR(255),
  PhoneNumber NVARCHAR(20),
  Email NVARCHAR(100),
  MedicalHistory NVARCHAR(MAX)
);