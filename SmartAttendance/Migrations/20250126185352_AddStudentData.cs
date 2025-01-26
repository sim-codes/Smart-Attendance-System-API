using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "DepartmentId", "LevelId", "MatriculationNumber", "UserId" },
                values: new object[] { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "21/0611", "fca7f097-444b-43ea-9bb4-c94243d09021" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
