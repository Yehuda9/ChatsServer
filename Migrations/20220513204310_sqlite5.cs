using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class sqlite5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    user1Id = table.Column<string>(type: "TEXT", nullable: false),
                    user2Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<string>(type: "TEXT", nullable: false),
                    fullName = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    nickName = table.Column<string>(type: "TEXT", nullable: false),
                    server = table.Column<string>(type: "TEXT", nullable: false),
                    last = table.Column<string>(type: "TEXT", nullable: false),
                    lastDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    MessageId = table.Column<string>(type: "TEXT", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    content = table.Column<string>(type: "TEXT", nullable: true),
                    sent = table.Column<bool>(type: "INTEGER", nullable: false),
                    fromId = table.Column<string>(type: "TEXT", nullable: false),
                    toId = table.Column<string>(type: "TEXT", nullable: false),
                    chatId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_messages_chats_chatId",
                        column: x => x.chatId,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    userMessagesid = table.Column<string>(type: "TEXT", nullable: false),
                    usersuserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => new { x.userMessagesid, x.usersuserId });
                    table.ForeignKey(
                        name: "FK_ChatUser_chats_userMessagesid",
                        column: x => x.userMessagesid,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_users_usersuserId",
                        column: x => x.usersuserId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_usersuserId",
                table: "ChatUser",
                column: "usersuserId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_chatId",
                table: "messages",
                column: "chatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "chats");
        }
    }
}
