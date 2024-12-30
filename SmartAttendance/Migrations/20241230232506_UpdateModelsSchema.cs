using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LevelId1",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LevelId1",
                table: "Courses",
                column: "LevelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Level_LevelId1",
                table: "Courses",
                column: "LevelId1",
                principalTable: "Level",
                principalColumn: "LevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Level_LevelId1",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LevelId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LevelId1",
                table: "Courses");
        }
    }
}
