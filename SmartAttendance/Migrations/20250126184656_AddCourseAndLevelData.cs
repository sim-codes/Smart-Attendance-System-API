using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseAndLevelData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassromId", "BottomLeftLat", "BottomLeftLon", "BottomRightLat", "BottomRightLon", "FacultyId", "Name", "TopLeftLat", "TopLeftLon", "TopRightLat", "TopRightLon" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce4"), 6.8920000000000003, 3.7240000000000002, 6.8920000000000003, 3.7244999999999999, new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"), "Engineering Lab 2", 6.8925000000000001, 3.7240000000000002, 6.8925000000000001, 3.7244999999999999 },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 6.8914999999999997, 3.7229999999999999, 6.8914999999999997, 3.7235, new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"), "Engineering Lab 1", 6.8920000000000003, 3.7229999999999999, 6.8920000000000003, 3.7235 }
                });

            migrationBuilder.InsertData(
                table: "Level",
                columns: new[] { "LevelId", "Name" },
                values: new object[,]
                {
                    { new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), "100" },
                    { new Guid("b2d4c053-49b6-410c-bc78-2d54a9991870"), "200" },
                    { new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"), "300" },
                    { new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"), "400" }
                });

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
                table: "Classrooms",
                keyColumn: "ClassromId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce4"));

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassromId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

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

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelId",
                keyValue: new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelId",
                keyValue: new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelId",
                keyValue: new Guid("b2d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelId",
                keyValue: new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
