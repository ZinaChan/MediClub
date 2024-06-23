CREATE DATABASE MediClubDb
USE MediClubDb

CREATE TABLE Departments (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Specializations (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    DepartmentId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);

CREATE TABLE Patients (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(50) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

CREATE TABLE Doctors (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,
    DepartmentId INT NOT NULL,
    SpecializationId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    FOREIGN KEY (SpecializationId) REFERENCES Specializations(Id)
);

CREATE TABLE Rooms (
    Id INT PRIMARY KEY IDENTITY,
    RoomNumber NVARCHAR(50) NOT NULL,
    DepartmentId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);

CREATE TABLE Appointments (
    Id INT PRIMARY KEY IDENTITY,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    RoomId INT NOT NULL,
    Date DATE NOT NULL,
    Time TIME NOT NULL,
    Reason NVARCHAR(200) NOT NULL,
    FOREIGN KEY (PatientId) REFERENCES Patients(Id),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(Id),
    FOREIGN KEY (RoomId) REFERENCES Rooms(Id)
);

CREATE TABLE MedicalRecords (
    Id INT PRIMARY KEY IDENTITY,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    Date DATE NOT NULL,
    Diagnosis NVARCHAR(200) NOT NULL,
    Treatment NVARCHAR(200) NOT NULL,
    FOREIGN KEY (PatientId) REFERENCES Patients(Id),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(Id)
);

CREATE TABLE Logs (
    Id INT PRIMARY KEY IDENTITY,
    Url NVARCHAR(200) NULL,
    RequestBody NVARCHAR(MAX) NULL,
    ResponsetBody NVARCHAR(MAX) NULL,
    CreationDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    StatusCode INT NOT NULL,
    HttpMethod NVARCHAR(50) NULL
);

