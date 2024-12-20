using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Faculty",
                columns: new[] { "FacultyId", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"), "FOE", "Faculty of Engineering" },
                    { new Guid("b4e2e3f2-3b3d-4b5d-8b0b-2b2b2b2b2b2b"), "FOS", "Faculty of Science" }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentId", "Code", "FacultyId", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "CSC", new Guid("b4e2e3f2-3b3d-4b5d-8b0b-2b2b2b2b2b2b"), "Computer Science" },
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "EEE", new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"), "Electrical Engineering" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Faculty",
                keyColumn: "FacultyId",
                keyValue: new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"));

            migrationBuilder.DeleteData(
                table: "Faculty",
                keyColumn: "FacultyId",
                keyValue: new Guid("b4e2e3f2-3b3d-4b5d-8b0b-2b2b2b2b2b2b"));
        }
    }
}
