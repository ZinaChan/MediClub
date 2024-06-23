USE MediClubDb

INSERT INTO Departments (Name) VALUES 
('Cardiology'),
('Neurology'),
('Oncology'),
('Pediatrics');

INSERT INTO Specializations (Name, DepartmentId) VALUES 
('Cardiologist', 1),
('Neurologist', 2),
('Oncologist', 3),
('Pediatrician', 4);

INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, Address, PhoneNumber, Email) VALUES 
('John', 'Doe', '1980-01-01', 'Male', '123 Main St', '555-555-5555', 'john.doe@example.com'),
('Jane', 'Smith', '1985-05-15', 'Female', '456 Elm St', '555-555-5556', 'jane.smith@example.com');

INSERT INTO Doctors (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, DepartmentId, SpecializationId) VALUES 
('Alice', 'Johnson', '1975-02-20', 'Female', 'alice.johnson@example.com', '555-555-5557', 1, 1),
('Bob', 'Brown', '1980-08-30', 'Male', 'bob.brown@example.com', '555-555-5558', 2, 2);

INSERT INTO Rooms (RoomNumber, DepartmentId) VALUES 
('101', 1),
('102', 1),
('201', 2),
('202', 2);

INSERT INTO Appointments (PatientId, DoctorId, RoomId, Date, Time, Reason) VALUES 
(1, 1, 1, '2024-07-01', '10:00', 'Routine Checkup'),
(2, 2, 3, '2024-07-02', '11:00', 'Neurological Consultation');

INSERT INTO MedicalRecords (PatientId, DoctorId, Date, Diagnosis, Treatment) VALUES 
(1, 1, '2024-06-01', 'Hypertension', 'Medication'),
(2, 2, '2024-06-15', 'Migraine', 'Rest and Medication');

INSERT INTO Logs (Url, RequestBody, ResponsetBody, CreationDate, EndDate, StatusCode, HttpMethod) VALUES 
('/api/patients', '{...}', '{...}', '2024-06-01 10:00', '2024-06-01 10:01', 200, 'POST'),
('/api/doctors', '{...}', '{...}', '2024-06-02 11:00', '2024-06-02 11:02', 200, 'POST');
