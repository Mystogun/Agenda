using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LawAgendaApi.Migrations
{
    public partial class ExtendedCaseTimeline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "IsPleading",
                table: "CaseTimelines",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PleadingDate",
                table: "CaseTimelines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPleading",
                table: "CaseTimelines");

            migrationBuilder.DropColumn(
                name: "PleadingDate",
                table: "CaseTimelines");
        }
    }
}
