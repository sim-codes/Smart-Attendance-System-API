using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInitialStudentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                column: "UserId",
                value: new Guid("a7552f15-c547-4d98-8ed8-f6461d459e82"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                column: "UserId",
                value: new Guid("b1d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
