using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediClubApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsetBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    HttpMethod = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RoomNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specializations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    AvatarUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_RoomId",
                table: "Appointments",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorId",
                table: "MedicalRecords",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DepartmentId",
                table: "Rooms",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_DepartmentId",
                table: "Specializations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SpecializationId",
                table: "Users",
                column: "SpecializationId");

            migrationBuilder.InsertData(
               table: "Departments",
               columns: new[] { "Id", "Name" },
               values: new object[,]
               {
                    { new Guid("1fa7524d-f93a-4686-92f0-5081f640c3fd"), "Cardiology" },
                    { new Guid("8e287ea2-8842-4ef3-9bea-0a10e8376ebb"), "Neurology" }
               });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "Password", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { new Guid("0570c29c-727d-410b-bbb2-405ded5eb29f"), "789 Oak St", "Assets/UsersImg/0570c29c-727d-410b-bbb2-405ded5eb29f.jpg", new DateTime(2003, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "Admin", "Female", "Adminovovich", "SecretAdmin", "555-555-5557", "Admin" },
                    { new Guid("06e898c1-a17c-4960-b188-e0ceb7d88bd6"), "321 Pin St", "Assets/UsersImg/06e898c1-a17c-4960-b188-e0ceb7d88bd6.jpg", new DateTime(1980, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "laila.brown@gmail.com", "Laila", "Female", "Brown", "Password123", "555-555-5558", "Doctor" },
                    { new Guid("47c9c66f-e485-4de6-8975-3b55588d0053"), "321 Pie St", "Assets/UsersImg/47c9c66f-e485-4de6-8975-3b55588d0053.jpg", new DateTime(1970, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.Marley@gmail.com", "Bob", "Male", "Marley", "Password123", "555-555-1234", "Doctor" },
                    { new Guid("367be6f5-0b8a-4f4c-85db-0f2fbc190d5e"), "456 Elm St", "Assets/UsersImg/367be6f5-0b8a-4f4c-85db-0f2fbc190d5e.jpg", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@gmail.com", "Jane", "Female", "Smith", "Password123", "555-555-5556", "Patient" },
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
                table: "Specializations",
                columns: new[] { "Id", "DepartmentId", "Name" },
                values: new object[,]
                {
                    { new Guid("02081457-05d7-4802-bdf4-42fb09ad0cf5"), new Guid("8e287ea2-8842-4ef3-9bea-0a10e8376ebb"), "Neurologist" },
                    { new Guid("ac16b376-d193-4d54-a482-8065ed4d77b9"), new Guid("1fa7524d-f93a-4686-92f0-5081f640c3fd"), "Cardiologist" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Date", "DoctorId", "PatientId", "Reason", "RoomId", "Time" },
                values: new object[,]
                {
                    { new Guid("0fb6dab2-a0c1-41ae-9f78-cfaeed2db908"), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("06e898c1-a17c-4960-b188-e0ceb7d88bd6"), new Guid("47c9c66f-e485-4de6-8975-3b55588d0053"), "Routine Checkup", new Guid("1ab29275-24a3-47ed-aa4d-f6d6c87ccee8"), new TimeSpan(0, 10, 0, 0, 0) },
                    { new Guid("cdb57a3a-5ce9-43c2-8364-5015dcd2df59"), new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0570c29c-727d-410b-bbb2-405ded5eb29f"), new Guid("367be6f5-0b8a-4f4c-85db-0f2fbc190d5e"), "Neurological Consultation", new Guid("ad96fc99-d0ac-4c2f-a35e-83a998dff1f1"), new TimeSpan(0, 11, 0, 0, 0) }
                });

        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}


