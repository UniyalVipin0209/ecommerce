using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beml.ECommerce.App.Migrations
{
    public partial class addProductTableChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");
        }
    }
}
