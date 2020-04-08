using Microsoft.EntityFrameworkCore.Migrations;

namespace LawAgendaApi.Migrations
{
    public partial class ExtendedCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourtHouse",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Defendant",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Judge",
                table: "Cases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourtHouse",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Defendant",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Judge",
                table: "Cases");
        }
    }
}
