using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADHD_Web.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLevelRecordModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "LevelRecords");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "LevelRecords");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "LevelRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "LevelRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "LevelRecords");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "LevelRecords");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "LevelRecords",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "LevelRecords",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
