using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Migrations
{
    public partial class try1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_fromId",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_toId",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_fromId",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_toId",
                table: "messages");

            migrationBuilder.AddColumn<string>(
                name: "MessageId",
                table: "users",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "toId",
                table: "messages",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "fromId",
                table: "messages",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "toId",
                table: "messages",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "fromId",
                table: "messages",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_messages_fromId",
                table: "messages",
                column: "fromId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_toId",
                table: "messages",
                column: "toId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_fromId",
                table: "messages",
                column: "fromId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_toId",
                table: "messages",
                column: "toId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
