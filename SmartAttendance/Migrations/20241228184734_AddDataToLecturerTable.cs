using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToLecturerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "LecturerId", "DepartmentId", "StaffId", "UserId" },
                values: new object[] { new Guid("8f47b996-12c3-4178-a69f-412b56b8f155"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "AU22/07623", "0da45a2c-7dda-490e-88ac-ff99fbef5a6a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lecturers",
                keyColumn: "LecturerId",
                keyValue: new Guid("8f47b996-12c3-4178-a69f-412b56b8f155"));
        }
    }
}
