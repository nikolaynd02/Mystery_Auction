using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MysteryAuction.Data.Migrations
{
    public partial class Many_To_Many_For_User_And_UC_For_User_And_MP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MysteryAuctionUserMysteryProduct",
                columns: table => new
                {
                    ParticipantsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserMysteryProductsParticipationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MysteryAuctionUserMysteryProduct", x => new { x.ParticipantsId, x.UserMysteryProductsParticipationId });
                    table.ForeignKey(
                        name: "FK_MysteryAuctionUserMysteryProduct_AspNetUsers_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MysteryAuctionUserMysteryProduct_MysteryProducts_UserMysteryProductsParticipationId",
                        column: x => x.UserMysteryProductsParticipationId,
                        principalTable: "MysteryProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MysteryAuctionUserUnclaimedContainer",
                columns: table => new
                {
                    ParticipantsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserUnclaimedContainersParticipationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MysteryAuctionUserUnclaimedContainer", x => new { x.ParticipantsId, x.UserUnclaimedContainersParticipationId });
                    table.ForeignKey(
                        name: "FK_MysteryAuctionUserUnclaimedContainer_AspNetUsers_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MysteryAuctionUserUnclaimedContainer_UnclaimedContainers_UserUnclaimedContainersParticipationId",
                        column: x => x.UserUnclaimedContainersParticipationId,
                        principalTable: "UnclaimedContainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MysteryAuctionUserMysteryProduct_UserMysteryProductsParticipationId",
                table: "MysteryAuctionUserMysteryProduct",
                column: "UserMysteryProductsParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_MysteryAuctionUserUnclaimedContainer_UserUnclaimedContainersParticipationId",
                table: "MysteryAuctionUserUnclaimedContainer",
                column: "UserUnclaimedContainersParticipationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MysteryAuctionUserMysteryProduct");

            migrationBuilder.DropTable(
                name: "MysteryAuctionUserUnclaimedContainer");
        }
    }
}
