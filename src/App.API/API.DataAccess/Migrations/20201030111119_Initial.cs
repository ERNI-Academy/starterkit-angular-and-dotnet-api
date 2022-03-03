using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.API.DataAccess.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Items",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Items", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Languages",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(nullable: false),
                Localization = table.Column<string>(nullable: false),
                IsActive = table.Column<bool>(nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Languages", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                FirstName = table.Column<string>(nullable: false),
                LastName = table.Column<string>(nullable: true),
                Role = table.Column<int>(nullable: false),
                Username = table.Column<string>(nullable: false),
                PasswordHash = table.Column<string>(nullable: false),
                IsDefault = table.Column<bool>(nullable: false),
                Created = table.Column<DateTime>(nullable: false),
                Updated = table.Column<DateTime>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Tag",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Label = table.Column<string>(nullable: true),
                Count = table.Column<int>(nullable: false),
                ItemId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tag", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tag_Items_ItemId",
                    column: x => x.ItemId,
                    principalTable: "Items",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RefreshToken",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<int>(nullable: false),
                Token = table.Column<string>(nullable: true),
                Expires = table.Column<DateTime>(nullable: false),
                Created = table.Column<DateTime>(nullable: false),
                CreatedByIp = table.Column<string>(nullable: true),
                Revoked = table.Column<DateTime>(nullable: true),
                RevokedByIp = table.Column<string>(nullable: true),
                ReplacedByToken = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RefreshToken", x => x.Id);
                table.ForeignKey(
                    name: "FK_RefreshToken_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_RefreshToken_UserId",
            table: "RefreshToken",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Tag_ItemId",
            table: "Tag",
            column: "ItemId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Languages");

        migrationBuilder.DropTable(
            name: "RefreshToken");

        migrationBuilder.DropTable(
            name: "Tag");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "Items");
    }
}
