using Microsoft.EntityFrameworkCore.Migrations;

namespace LawAgendaApi.Migrations
{
    public partial class EditedTimeline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_CaseTimelines_CaseTimelineId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_CaseTimelineId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CaseTimelineId",
                table: "Files");

            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "CaseTimelines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseTimelines_FileId",
                table: "CaseTimelines",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseTimelines_Files_FileId",
                table: "CaseTimelines",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseTimelines_Files_FileId",
                table: "CaseTimelines");

            migrationBuilder.DropIndex(
                name: "IX_CaseTimelines_FileId",
                table: "CaseTimelines");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "CaseTimelines");

            migrationBuilder.AddColumn<long>(
                name: "CaseTimelineId",
                table: "Files",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_CaseTimelineId",
                table: "Files",
                column: "CaseTimelineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_CaseTimelines_CaseTimelineId",
                table: "Files",
                column: "CaseTimelineId",
                principalTable: "CaseTimelines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
