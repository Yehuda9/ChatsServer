using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class try4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_chats_userMessagesId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_chats_ChatId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Message",
                newName: "Chatid");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ChatId",
                table: "Message",
                newName: "IX_Message_Chatid");

            migrationBuilder.RenameColumn(
                name: "userMessagesId",
                table: "ChatUser",
                newName: "userMessagesid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "chats",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "Chatid",
                table: "Message",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "userMessagesid",
                table: "ChatUser",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chats",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_chats_userMessagesid",
                table: "ChatUser",
                column: "userMessagesid",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_chats_Chatid",
                table: "Message",
                column: "Chatid",
                principalTable: "chats",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_chats_userMessagesid",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_chats_Chatid",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "Chatid",
                table: "Message",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_Chatid",
                table: "Message",
                newName: "IX_Message_ChatId");

            migrationBuilder.RenameColumn(
                name: "userMessagesid",
                table: "ChatUser",
                newName: "userMessagesId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "chats",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "Message",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "userMessagesId",
                table: "ChatUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "chats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_chats_userMessagesId",
                table: "ChatUser",
                column: "userMessagesId",
                principalTable: "chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_chats_ChatId",
                table: "Message",
                column: "ChatId",
                principalTable: "chats",
                principalColumn: "Id");
        }
    }
}
