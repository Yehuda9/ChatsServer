using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class t : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "last");

            migrationBuilder.AddColumn<string>(
                name: "fullName",
                table: "users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "lastDate",
                table: "users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fullName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "lastDate",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "last",
                table: "users",
                newName: "name");
        }
    }
}
