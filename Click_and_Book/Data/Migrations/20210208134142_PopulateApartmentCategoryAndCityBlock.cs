using Microsoft.EntityFrameworkCore.Migrations;

namespace Click_and_Book.Data.Migrations
{
    public partial class PopulateApartmentCategoryAndCityBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[ApartmentCategories] ([Name]) 
                                     VALUES 
                                            (N'Hotel'), 
                                            (N'Hostel'), 
                                            (N'Motel'),
                                            (N'Villa'),
                                            (N'Guest Hose'),
                                            (N'Apartmnent'),
                                            (N'Studio Apartment'),
                                            (N'Room');

                                   INSERT INTO[dbo].[CityBlocks] ([Name]) 
                                    VALUES
                                            (N'Bačvice'),
                                            (N'Blatine-Škrape'),
                                            (N'Bol'),
                                            (N'Brda'),
                                            (N'Grad'),
                                            (N'Gripe'),
                                            (N'Kman'),
                                            (N'Kocunar'),
                                            (N'Lokve'),
                                            (N'Lovret'),
                                            (N'Lučac-Manuš'),
                                            (N'Mejaši'),
                                            (N'Meje'),
                                            (N'Mertojak'),
                                            (N'Neslanovac'),
                                            (N'Plokite'),
                                            (N'Pujanke'),
                                            (N'Ravne njive'),
                                            (N'Sirobuja'),
                                            (N'Spinut'),
                                            (N'Split 3'),
                                            (N'Sućidar'),
                                            (N'Trstenik'),
                                            (N'Varoš'),
                                            (N'Visoka'),
                                            (N'Žnjan-Pazigrad');
                                            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
