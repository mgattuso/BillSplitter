using Microsoft.EntityFrameworkCore.Migrations;

namespace BillSplitter.Migrations
{
    public partial class Addnumberofsplits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfSplits",
                table: "Bill",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfSplits",
                table: "Bill");
        }
    }
}
