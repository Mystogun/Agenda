using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LawAgendaApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "A1CaseTypes",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    IsDeleted = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    Type = table.Column<string>(maxLength: 255, nullable: true),
                    Color = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A1CaseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "A1FileTypes",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    IsDeleted = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    Type = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A1FileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "A1UserTypes",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    IsDeleted = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    Type = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A1UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    IsDeleted = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber2 = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    IsDeleted = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    IsPrivate = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    IsClosed = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    Number = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    StartingDate = table.Column<DateTime>(nullable: true),
                    NextDate = table.Column<DateTime>(nullable: true),
                    NotificationDate = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<long>(nullable: true),
                    TypeId = table.Column<short>(nullable: true),
                    Price = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cases_A1CaseTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "A1CaseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    IsDeleted = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    Path = table.Column<string>(maxLength: 512, nullable: true),
                    Extension = table.Column<string>(maxLength: 10, nullable: true),
                    TypeId = table.Column<short>(nullable: true),
                    CaseTimelineId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_A1FileTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "A1FileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    IsDeleted = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber2 = table.Column<string>(maxLength: 50, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<string>(nullable: true),
                    PasswordAccess = table.Column<string>(nullable: true),
                    TypeId = table.Column<short>(nullable: true),
                    FileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Users_A1UserTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "A1UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CaseTimelines",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "DateAdd(Hour, 3, GetUtcDate())"),
                    IsDeleted = table.Column<byte>(nullable: false, defaultValueSql: "0"),
                    Notes = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    CaseId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTimelines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseTimelines_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CaseTimelines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CustomerId",
                table: "Cases",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_TypeId",
                table: "Cases",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTimelines_CaseId",
                table: "CaseTimelines",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTimelines_UserId",
                table: "CaseTimelines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CaseTimelineId",
                table: "Files",
                column: "CaseTimelineId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_TypeId",
                table: "Files",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FileId",
                table: "Users",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TypeId",
                table: "Users",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_CaseTimelines_CaseTimelineId",
                table: "Files",
                column: "CaseTimelineId",
                principalTable: "CaseTimelines",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Customers_CustomerId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_A1CaseTypes_TypeId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseTimelines_Cases_CaseId",
                table: "CaseTimelines");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseTimelines_Users_UserId",
                table: "CaseTimelines");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "A1CaseTypes");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "A1UserTypes");

            migrationBuilder.DropTable(
                name: "CaseTimelines");

            migrationBuilder.DropTable(
                name: "A1FileTypes");
        }
    }
}
