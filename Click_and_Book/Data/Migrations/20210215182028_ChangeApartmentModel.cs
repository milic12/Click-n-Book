using Microsoft.EntityFrameworkCore.Migrations;

namespace Click_and_Book.Data.Migrations
{
    public partial class ChangeApartmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_CityBlocks_CityBlockId",
                table: "Apartments");

            migrationBuilder.AlterColumn<int>(
                name: "CityBlockId",
                table: "Apartments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_CityBlocks_CityBlockId",
                table: "Apartments",
                column: "CityBlockId",
                principalTable: "CityBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_CityBlocks_CityBlockId",
                table: "Apartments");

            migrationBuilder.AlterColumn<int>(
                name: "CityBlockId",
                table: "Apartments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_CityBlocks_CityBlockId",
                table: "Apartments",
                column: "CityBlockId",
                principalTable: "CityBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
