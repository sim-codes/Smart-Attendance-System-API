using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleFieldUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_AcademicSessions_SessionId",
                table: "ClassSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_ClassSchedules_SessionId",
                table: "ClassSchedules");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "ClassSchedules");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ClassSchedules",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_UserId",
                table: "ClassSchedules",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_AspNetUsers_UserId",
                table: "ClassSchedules",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_AspNetUsers_UserId",
                table: "ClassSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_ClassSchedules_UserId",
                table: "ClassSchedules");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClassSchedules");

            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "ClassSchedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_SessionId",
                table: "ClassSchedules",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_AcademicSessions_SessionId",
                table: "ClassSchedules",
                column: "SessionId",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
