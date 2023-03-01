using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beml.ECommerce.App.Migrations
{
    public partial class addApplicationTypeInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicationTypes_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "ApplicationTypes",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicationTypes_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeId",
                table: "Products");
        }
    }
}
