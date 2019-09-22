using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class AddBenefitsToJobModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Jobs",
                newName: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Benefits",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Benefits",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Jobs",
                newName: "City");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Jobs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Jobs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
