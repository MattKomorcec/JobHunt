using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class ChangeJobModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_UserId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_UserId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Jobs",
                newName: "JobId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "ApplicationUserId", "DateAdded", "Description", "Status", "Title", "UserId" },
                values: new object[] { 1, null, new DateTime(2019, 9, 3, 21, 27, 52, 813, DateTimeKind.Local).AddTicks(7544), "This is just a sample job", 0, "First Job", "ccba5f41-69f8-4e61-90ed-b9afd19b7ecf" });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ApplicationUserId",
                table: "Jobs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_ApplicationUserId",
                table: "Jobs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_ApplicationUserId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_ApplicationUserId",
                table: "Jobs");

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "JobId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Jobs",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UserId",
                table: "Jobs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_UserId",
                table: "Jobs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
