using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedClassroomData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Department_DepartmentId",
                table: "Classroom");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Classroom",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Classroom_DepartmentId",
                table: "Classroom",
                newName: "IX_Classroom_FacultyId");

            migrationBuilder.UpdateData(
                table: "Classroom",
                keyColumn: "ClassromId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce4"),
                columns: new[] { "FacultyId", "Name" },
                values: new object[] { new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"), "Engineering Lab 2" });

            migrationBuilder.UpdateData(
                table: "Classroom",
                keyColumn: "ClassromId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "FacultyId", "Name" },
                values: new object[] { new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"), "Engineering Lab 1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Faculty_FacultyId",
                table: "Classroom",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Faculty_FacultyId",
                table: "Classroom");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Classroom",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Classroom_FacultyId",
                table: "Classroom",
                newName: "IX_Classroom_DepartmentId");

            migrationBuilder.UpdateData(
                table: "Classroom",
                keyColumn: "ClassromId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce4"),
                columns: new[] { "DepartmentId", "Name" },
                values: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "EE Lab 1" });

            migrationBuilder.UpdateData(
                table: "Classroom",
                keyColumn: "ClassromId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "DepartmentId", "Name" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "CSC Lab 1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Department_DepartmentId",
                table: "Classroom",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
