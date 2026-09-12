using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureYouAi.Migrations
{
    /// <inheritdoc />
    public partial class migAddMessageReplyFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageReplies",
                columns: table => new
                {
                    MessageReplyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    ReplyDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSentSuccessfully = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReplies", x => x.MessageReplyId);
                    table.ForeignKey(
                        name: "FK_MessageReplies_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageReplies_MessageId",
                table: "MessageReplies",
                column: "MessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageReplies");
        }
    }
}
