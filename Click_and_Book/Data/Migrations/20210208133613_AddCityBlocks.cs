using Microsoft.EntityFrameworkCore.Migrations;

namespace Click_and_Book.Data.Migrations
{
    public partial class AddCityBlocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Apartments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityBlockId",
                table: "Apartments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CityBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityBlocks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_CityBlockId",
                table: "Apartments",
                column: "CityBlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_CityBlocks_CityBlockId",
                table: "Apartments",
                column: "CityBlockId",
                principalTable: "CityBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_CityBlocks_CityBlockId",
                table: "Apartments");

            migrationBuilder.DropTable(
                name: "CityBlocks");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_CityBlockId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "CityBlockId",
                table: "Apartments");
        }
    }
}
