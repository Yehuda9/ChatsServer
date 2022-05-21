using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class addProImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profileImgid",
                table: "users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Img",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    image = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Img", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_profileImgid",
                table: "users",
                column: "profileImgid");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Img_profileImgid",
                table: "users",
                column: "profileImgid",
                principalTable: "Img",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Img_profileImgid",
                table: "users");

            migrationBuilder.DropTable(
                name: "Img");

            migrationBuilder.DropIndex(
                name: "IX_users_profileImgid",
                table: "users");

            migrationBuilder.DropColumn(
                name: "profileImgid",
                table: "users");
        }
    }
}
