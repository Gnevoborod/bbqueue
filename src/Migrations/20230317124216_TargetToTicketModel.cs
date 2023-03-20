using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bbqueue.Migrations
{
    /// <inheritdoc />
    public partial class TargetToTicketModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "target_id",
                table: "ticket",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "target_id",
                table: "ticket");
        }
    }
}
