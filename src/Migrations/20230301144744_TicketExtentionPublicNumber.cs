using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bbqueue.Migrations
{
    /// <inheritdoc />
    public partial class TicketExtentionPublicNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "public_number",
                table: "ticket",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "public_number",
                table: "ticket");
        }
    }
}
