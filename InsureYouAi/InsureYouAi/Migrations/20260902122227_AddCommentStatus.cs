using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureYouAi.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentStatus",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentStatus",
                table: "Comments");
        }
    }
}
