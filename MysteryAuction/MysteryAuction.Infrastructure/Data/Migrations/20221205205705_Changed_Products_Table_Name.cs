using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MysteryAuction.Data.Migrations
{
    public partial class Changed_Products_Table_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_MysteryProducts_ProductId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_MysteryProducts_AspNetUsers_BuyerId",
                table: "MysteryProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_MysteryProducts_AspNetUsers_SellerId",
                table: "MysteryProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_MysteryProducts_ProductsCategories_CategoryId",
                table: "MysteryProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MysteryProducts",
                table: "MysteryProducts");

            migrationBuilder.RenameTable(
                name: "MysteryProducts",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_MysteryProducts_SellerId",
                table: "Products",
                newName: "IX_Products_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_MysteryProducts_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MysteryProducts_BuyerId",
                table: "Products",
                newName: "IX_Products_BuyerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Products_ProductId",
                table: "Bids",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_BuyerId",
                table: "Products",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductsCategories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "ProductsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Products_ProductId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_BuyerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductsCategories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "MysteryProducts");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerId",
                table: "MysteryProducts",
                newName: "IX_MysteryProducts_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "MysteryProducts",
                newName: "IX_MysteryProducts_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BuyerId",
                table: "MysteryProducts",
                newName: "IX_MysteryProducts_BuyerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MysteryProducts",
                table: "MysteryProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_MysteryProducts_ProductId",
                table: "Bids",
                column: "ProductId",
                principalTable: "MysteryProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MysteryProducts_AspNetUsers_BuyerId",
                table: "MysteryProducts",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MysteryProducts_AspNetUsers_SellerId",
                table: "MysteryProducts",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MysteryProducts_ProductsCategories_CategoryId",
                table: "MysteryProducts",
                column: "CategoryId",
                principalTable: "ProductsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
