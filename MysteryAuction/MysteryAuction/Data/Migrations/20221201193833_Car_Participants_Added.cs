using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MysteryAuction.Data.Migrations
{
    public partial class Car_Participants_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CarId",
                table: "AspNetUsers",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cars_CarId",
                table: "AspNetUsers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cars_CarId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CarId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "AspNetUsers");
        }
    }
}
