using Microsoft.EntityFrameworkCore.Migrations;

namespace Click_and_Book.Data.Migrations
{
    public partial class AddClientAndOwnerRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) 
                                     VALUES (N'd9152571-90b4-4e40-b3c2-e7aa8c34f731', N'Owner', N'OWNER', N'41e0a43d-052e-4329-bbfb-d3cc5e854373')
                                   INSERT INTO[dbo].[AspNetRoles]([Id], [Name], [NormalizedName], [ConcurrencyStamp]) 
                                    VALUES(N'ff030333-8fd4-4b4e-b3f1-3dd903cb5c10', N'Client', N'CLIENT', N'510c6a1c-68d7-45bd-966c-5387a55072f8')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
