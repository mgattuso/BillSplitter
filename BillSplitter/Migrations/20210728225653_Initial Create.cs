using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BillSplitter.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomMate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomMate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(maxLength: 140, nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    PaidDate = table.Column<DateTime>(nullable: false),
                    PaidByRoomMateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_RoomMate_PaidByRoomMateId",
                        column: x => x.PaidByRoomMateId,
                        principalTable: "RoomMate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BillId = table.Column<int>(nullable: false),
                    RoomMateId = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    PaidDate = table.Column<DateTime>(nullable: false),
                    Confirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_RoomMate_RoomMateId",
                        column: x => x.RoomMateId,
                        principalTable: "RoomMate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_PaidByRoomMateId",
                table: "Bill",
                column: "PaidByRoomMateId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BillId",
                table: "Payment",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RoomMateId",
                table: "Payment",
                column: "RoomMateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "RoomMate");
        }
    }
}
