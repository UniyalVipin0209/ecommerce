using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beml.ECommerce.App.Migrations
{
    public partial class addApplicationTypeInProductCorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicationTypes_CategoryId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ApplicationTypeId",
                table: "Products",
                column: "ApplicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicationTypes_ApplicationTypeId",
                table: "Products",
                column: "ApplicationTypeId",
                principalTable: "ApplicationTypes",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicationTypes_ApplicationTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ApplicationTypeId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicationTypes_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "ApplicationTypes",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
