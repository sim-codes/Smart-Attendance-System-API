using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Code", "CreditUnits", "DepartmentId", "LevelId", "LevelId1", "Title" },
                values: new object[,]
                {
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "CSC301", 3, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"), null, "Database Management Systems" },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"), "CSC101", 2, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), null, "Introduction to Computer Science" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52c"), "CSC201", 3, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), new Guid("b2d4c053-49b6-410c-bc78-2d54a9991870"), null, "Data Structures and Algorithms" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52c"));
        }
    }
}
