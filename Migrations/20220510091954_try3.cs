using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class try3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_userMessages_userMessagesId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_userMessages_ChatId",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userMessages",
                table: "userMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messages",
                table: "messages");

            migrationBuilder.RenameTable(
                name: "userMessages",
                newName: "chats");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_messages_ChatId",
                table: "Message",
                newName: "IX_Message_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chats",
                table: "chats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "MessageId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_chats_userMessagesId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_chats_ChatId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chats",
                table: "chats");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "messages");

            migrationBuilder.RenameTable(
                name: "chats",
                newName: "userMessages");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ChatId",
                table: "messages",
                newName: "IX_messages_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messages",
                table: "messages",
                column: "MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userMessages",
                table: "userMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_userMessages_userMessagesId",
                table: "ChatUser",
                column: "userMessagesId",
                principalTable: "userMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_userMessages_ChatId",
                table: "messages",
                column: "ChatId",
                principalTable: "userMessages",
                principalColumn: "Id");
        }
    }
}
