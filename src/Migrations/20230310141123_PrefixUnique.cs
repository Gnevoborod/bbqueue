using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bbqueue.Migrations
{
    /// <inheritdoc />
    public partial class PrefixUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_window_target_target_target_id",
                table: "window_target");

            migrationBuilder.DropIndex(
                name: "IX_window_target_target_id",
                table: "window_target");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "employee",
                newName: "role");

            migrationBuilder.AddColumn<long>(
                name: "target_id",
                table: "ticket_operation",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_amount_prefix",
                table: "ticket_amount",
                column: "prefix",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_target_prefix",
                table: "target",
                column: "prefix",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ticket_amount_prefix",
                table: "ticket_amount");

            migrationBuilder.DropIndex(
                name: "IX_target_prefix",
                table: "target");

            migrationBuilder.DropColumn(
                name: "target_id",
                table: "ticket_operation");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "employee",
                newName: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_window_target_target_id",
                table: "window_target",
                column: "target_id");

            migrationBuilder.AddForeignKey(
                name: "FK_window_target_target_target_id",
                table: "window_target",
                column: "target_id",
                principalTable: "target",
                principalColumn: "target_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
