using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartAttendance.Migrations
{
    /// <inheritdoc />
    public partial class InitialClassroomData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classroom",
                columns: table => new
                {
                    ClassromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    TopLeftLat = table.Column<double>(type: "float", nullable: false),
                    TopLeftLon = table.Column<double>(type: "float", nullable: false),
                    TopRightLat = table.Column<double>(type: "float", nullable: false),
                    TopRightLon = table.Column<double>(type: "float", nullable: false),
                    BottomLeftLat = table.Column<double>(type: "float", nullable: false),
                    BottomLeftLon = table.Column<double>(type: "float", nullable: false),
                    BottomRightLat = table.Column<double>(type: "float", nullable: false),
                    BottomRightLon = table.Column<double>(type: "float", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classroom", x => x.ClassromId);
                    table.ForeignKey(
                        name: "FK_Classroom_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classroom",
                columns: new[] { "ClassromId", "BottomLeftLat", "BottomLeftLon", "BottomRightLat", "BottomRightLon", "DepartmentId", "Name", "TopLeftLat", "TopLeftLon", "TopRightLat", "TopRightLon" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce4"), 6.8920000000000003, 3.7240000000000002, 6.8920000000000003, 3.7244999999999999, new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "EE Lab 1", 6.8925000000000001, 3.7240000000000002, 6.8925000000000001, 3.7244999999999999 },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 6.8914999999999997, 3.7229999999999999, 6.8914999999999997, 3.7235, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "CSC Lab 1", 6.8920000000000003, 3.7229999999999999, 6.8920000000000003, 3.7235 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_DepartmentId",
                table: "Classroom",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classroom");
        }
    }
}
