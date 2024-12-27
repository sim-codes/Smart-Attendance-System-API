using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentLevelData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelId",
                keyValue: new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
