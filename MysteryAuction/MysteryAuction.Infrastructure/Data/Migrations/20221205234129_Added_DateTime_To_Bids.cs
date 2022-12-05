using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MysteryAuction.Data.Migrations
{
    public partial class Added_DateTime_To_Bids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MadeAt",
                table: "Bids",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MadeAt",
                table: "Bids");
        }
    }
}
