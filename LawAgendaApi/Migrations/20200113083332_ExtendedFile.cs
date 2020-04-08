using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LawAgendaApi.Migrations
{
    public partial class ExtendedFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Files",
                nullable: false, 
                defaultValueSql: "NewId()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Files");
        }
    }
}
