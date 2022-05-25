using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class file : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Img_profileImgid",
                table: "users");

            migrationBuilder.DropTable(
                name: "Img");

            migrationBuilder.AddForeignKey(
                name: "FK_users_FileModel_profileImgid",
                table: "users",
                column: "profileImgid",
                principalTable: "FileModel",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_FileModel_profileImgid",
                table: "users");

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

            migrationBuilder.AddForeignKey(
                name: "FK_users_Img_profileImgid",
                table: "users",
                column: "profileImgid",
                principalTable: "Img",
                principalColumn: "id");
        }
    }
}
