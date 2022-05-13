using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class add_chatid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_chats_Chatid",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "messages");

            migrationBuilder.RenameColumn(
                name: "Chatid",
                table: "messages",
                newName: "chatId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_Chatid",
                table: "messages",
                newName: "IX_messages_chatId");

            migrationBuilder.UpdateData(
                table: "messages",
                keyColumn: "chatId",
                keyValue: null,
                column: "chatId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "chatId",
                table: "messages",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messages",
                table: "messages",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_chats_chatId",
                table: "messages",
                column: "chatId",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_chats_chatId",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messages",
                table: "messages");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "chatId",
                table: "Message",
                newName: "Chatid");

            migrationBuilder.RenameIndex(
                name: "IX_messages_chatId",
                table: "Message",
                newName: "IX_Message_Chatid");

            migrationBuilder.AlterColumn<string>(
                name: "Chatid",
                table: "Message",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_chats_Chatid",
                table: "Message",
                column: "Chatid",
                principalTable: "chats",
                principalColumn: "id");
        }
    }
}
