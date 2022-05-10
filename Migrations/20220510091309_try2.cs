using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class try2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_messages_MessageId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_MessageId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageId",
                table: "users",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_users_MessageId",
                table: "users",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_messages_MessageId",
                table: "users",
                column: "MessageId",
                principalTable: "messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
