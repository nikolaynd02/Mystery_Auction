using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MysteryAuction.Data.Migrations
{
    public partial class Many_To_Many_Car_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CarMysteryAuctionUser",
                columns: table => new
                {
                    ParticipantsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserCarsParticipationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarMysteryAuctionUser", x => new { x.ParticipantsId, x.UserCarsParticipationId });
                    table.ForeignKey(
                        name: "FK_CarMysteryAuctionUser_AspNetUsers_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarMysteryAuctionUser_Cars_UserCarsParticipationId",
                        column: x => x.UserCarsParticipationId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarMysteryAuctionUser_UserCarsParticipationId",
                table: "CarMysteryAuctionUser",
                column: "UserCarsParticipationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarMysteryAuctionUser");

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
    }
}
