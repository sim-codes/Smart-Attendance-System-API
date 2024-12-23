using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8f7d6e5c-4b3a-2f1d-9e8d-7c6b5a4f3e2d", null, "Student", "STUDENT" },
                    { "8f7d6e5c-4b3a-2f1d-9e8d-7c6b5a4f3e2e", null, "Lecturer", "LECTURER" },
                    { "8f7d6e5c-4b3a-2f1d-9e8d-7c6b5a4f3e2f", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f7d6e5c-4b3a-2f1d-9e8d-7c6b5a4f3e2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f7d6e5c-4b3a-2f1d-9e8d-7c6b5a4f3e2e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f7d6e5c-4b3a-2f1d-9e8d-7c6b5a4f3e2f");
        }
    }
}
