using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class UpdateJobModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "JobId",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Jobs",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Jobs",
                newName: "DateApplied");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Jobs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Jobs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Jobs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Jobs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DateApplied",
                table: "Jobs",
                newName: "DateAdded");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "ApplicationUserId", "DateAdded", "Description", "Status", "Title", "UserId" },
                values: new object[] { 1, null, new DateTime(2019, 9, 3, 21, 27, 52, 813, DateTimeKind.Local).AddTicks(7544), "This is just a sample job", 0, "First Job", "ccba5f41-69f8-4e61-90ed-b9afd19b7ecf" });
        }
    }
}
