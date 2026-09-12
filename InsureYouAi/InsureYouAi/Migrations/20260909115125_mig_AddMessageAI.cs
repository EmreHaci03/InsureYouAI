using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureYouAi.Migrations
{
    /// <inheritdoc />
    public partial class mig_AddMessageAI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AICategory",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AICategory",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Messages");
        }
    }
}
