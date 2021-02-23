using Microsoft.EntityFrameworkCore.Migrations;

namespace Click_and_Book.Data.Migrations
{
    public partial class CorrectSpelingmistakes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Balconiy",
                table: "Apartments");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Apartments",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Balcony",
                table: "Apartments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Balcony",
                table: "Apartments");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Balconiy",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
