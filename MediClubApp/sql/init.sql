create database MediClubDb

use MediClubDb

CREATE TABLE Doctor (
    Id INT PRIMARY KEY IDENTITY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Patient (
    Id INT PRIMARY KEY IDENTITY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    MedicalHistory TEXT
);
