using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class addFileMsg1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "formFile",
                table: "messages");

            migrationBuilder.AddColumn<string>(
                name: "formFileid",
                table: "messages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileModel",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    data = table.Column<byte[]>(type: "BLOB", nullable: false),
                    contentType = table.Column<string>(type: "TEXT", nullable: false),
                    length = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileModel", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_formFileid",
                table: "messages",
                column: "formFileid");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_FileModel_formFileid",
                table: "messages",
                column: "formFileid",
                principalTable: "FileModel",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_FileModel_formFileid",
                table: "messages");

            migrationBuilder.DropTable(
                name: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_messages_formFileid",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "formFileid",
                table: "messages");

            migrationBuilder.AddColumn<byte[]>(
                name: "formFile",
                table: "messages",
                type: "BLOB",
                nullable: true);
        }
    }
}
