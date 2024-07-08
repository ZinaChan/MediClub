using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MediClubApp.Migrations
{
    public partial class Initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1fa7524d-f93a-4686-92f0-5081f640c3fd"), "Cardiology" },
                    { new Guid("8e287ea2-8842-4ef3-9bea-0a10e8376ebb"), "Neurology" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "DepartmentId", "Name" },
                values: new object[,]
                {
                    { new Guid("02081457-05d7-4802-bdf4-42fb09ad0cf5"), new Guid("8e287ea2-8842-4ef3-9bea-0a10e8376ebb"), "Neurologist" },
                    { new Guid("ac16b376-d193-4d54-a482-8065ed4d77b9"), new Guid("1fa7524d-f93a-4686-92f0-5081f640c3fd"), "Cardiologist" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "Password", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { new Guid("0570c29c-727d-410b-bbb2-405ded5eb29f"), "789 Oak St", "Assets/UsersImg/0570c29c-727d-410b-bbb2-405ded5eb29f.jpg", new DateTime(2003, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "Admin", "Female", "Adminovovich", "SecretAdmin", "555-555-5557", "Admin" }, 
                    { new Guid("367be6f5-0b8a-4f4c-85db-0f2fbc190d5e"), "456 Elm St", "Assets/UsersImg/367be6f5-0b8a-4f4c-85db-0f2fbc190d5e.jpg", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@gmail.com", "Jane", "Female", "Smith", "Password123", "555-555-5556", "Patient" },
                });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "Password", "PhoneNumber", "Role", "DepartmentId" , "SpecializationId" },
                values: new object[,]
                { 
                    { new Guid("06e898c1-a17c-4960-b188-e0ceb7d88bd6"), "321 Pin St", "Assets/UsersImg/06e898c1-a17c-4960-b188-e0ceb7d88bd6.jpg", new DateTime(1980, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "laila.brown@gmail.com", "Laila", "Female", "Brown", "Password123", "555-555-5558", "Doctor", new Guid("1fa7524d-f93a-4686-92f0-5081f640c3fd"), new Guid("ac16b376-d193-4d54-a482-8065ed4d77b9") },
                    { new Guid("47c9c66f-e485-4de6-8975-3b55588d0053"), "321 Pie St", "Assets/UsersImg/47c9c66f-e485-4de6-8975-3b55588d0053.jpg", new DateTime(1970, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.Marley@gmail.com", "Bob", "Male", "Marley", "Password123", "555-555-1234", "Doctor", new Guid("8e287ea2-8842-4ef3-9bea-0a10e8376ebb"), new Guid("02081457-05d7-4802-bdf4-42fb09ad0cf5") }, 
                }); 
                
            migrationBuilder.InsertData(
                table: "MedicalRecords",
                columns: new[] { "Id", "Date", "Diagnosis", "DoctorId", "PatientId", "Treatment" },
                values: new object[,]
                {
                    { new Guid("838de7e0-c2aa-4407-8408-d26423061c0c"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hypertension", new Guid("06e898c1-a17c-4960-b188-e0ceb7d88bd6"), new Guid("47c9c66f-e485-4de6-8975-3b55588d0053"), "Medication" },
                    { new Guid("84daed9a-4e79-4998-9616-f97cb395eb50"), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Migraine", new Guid("0570c29c-727d-410b-bbb2-405ded5eb29f"), new Guid("367be6f5-0b8a-4f4c-85db-0f2fbc190d5e"), "Rest and Medication" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "DepartmentId", "RoomNumber" },
                values: new object[,]
                {
                    { new Guid("1ab29275-24a3-47ed-aa4d-f6d6c87ccee8"), new Guid("8e287ea2-8842-4ef3-9bea-0a10e8376ebb"), "102" },
                    { new Guid("2dec569a-ac62-41dc-8f57-fc51ce58df82"), new Guid("1fa7524d-f93a-4686-92f0-5081f640c3fd"), "201" },
                    { new Guid("ad96fc99-d0ac-4c2f-a35e-83a998dff1f1"), new Guid("1fa7524d-f93a-4686-92f0-5081f640c3fd"), "101" },
                    { new Guid("db1bb264-0d70-4a38-bbd8-d318ff7328a1"), new Guid("8e287ea2-8842-4ef3-9bea-0a10e8376ebb"), "202" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Date", "DoctorId", "PatientId", "Reason", "RoomId", "Time" },
                values: new object[,]
                {
                    { new Guid("0fb6dab2-a0c1-41ae-9f78-cfaeed2db908"), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("06e898c1-a17c-4960-b188-e0ceb7d88bd6"), new Guid("47c9c66f-e485-4de6-8975-3b55588d0053"), "Routine Checkup", new Guid("1ab29275-24a3-47ed-aa4d-f6d6c87ccee8"), new TimeSpan(0, 10, 0, 0, 0) },
                    { new Guid("cdb57a3a-5ce9-43c2-8364-5015dcd2df59"), new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0570c29c-727d-410b-bbb2-405ded5eb29f"), new Guid("367be6f5-0b8a-4f4c-85db-0f2fbc190d5e"), "Follow-up Consultation", new Guid("db1bb264-0d70-4a38-bbd8-d318ff7328a1"), new TimeSpan(0, 15, 0, 0, 0) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: new Guid("0fb6dab2-a0c1-41ae-9f78-cfaeed2db908"));

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: new Guid("cdb57a3a-5ce9-43c2-8364-5015dcd2df59"));

            migrationBuilder.DeleteData(
                table: "MedicalRecords",
                keyColumn: "Id",
                keyValue: new Guid("838de7e0-c2aa-4407-8408-d26423061c0c"));

            migrationBuilder.DeleteData(
                table: "MedicalRecords",
                keyColumn: "Id",
                keyValue: new Guid("84daed9a-4e79-4998-9616-f97cb395eb50"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("1ab29275-24a3-47ed-aa4d-f6d6c87ccee8"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("2dec569a-ac62-41dc-8f57-fc51ce58df82"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("ad96fc99-d0ac-4c2f-a35e-83a998dff1f1"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("db1bb264-0d70-4a38-bbd8-d318ff7328a1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0570c29c-727d-410b-bbb2-405ded5eb29f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("06e898c1-a17c-4960-b188-e0ceb7d88bd6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("367be6f5-0b8a-4f4c-85db-0f2fbc190d5e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("47c9c66f-e485-4de6-8975-3b55588d0053"));

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("02081457-05d7-4802-bdf4-42fb09ad0cf5"));

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("ac16b376-d193-4d54-a482-8065ed4d77b9"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("1fa7524d-f93a-4686-92f0-5081f640c3fd"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("8e287ea2-8842-4ef3-9bea-0a10e8376ebb"));
        }
    }
}
