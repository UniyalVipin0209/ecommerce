using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beml.ECommerce.App.Migrations
{
    public partial class addShortDescInProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDesc",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDesc",
                table: "Products");
        }
    }
}
