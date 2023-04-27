using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace bbqueue.Migrations
{
    /// <inheritdoc />
    public partial class CutTheMonolith : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticket_operation_employee_employee_id",
                table: "ticket_operation");

            migrationBuilder.DropForeignKey(
                name: "FK_window_employee_employee_id",
                table: "window");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropIndex(
                name: "IX_window_employee_id",
                table: "window");

            migrationBuilder.DropIndex(
                name: "IX_ticket_operation_employee_id",
                table: "ticket_operation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    external_system_id = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employee_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_window_employee_id",
                table: "window",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_operation_employee_id",
                table: "ticket_operation",
                column: "employee_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_operation_employee_employee_id",
                table: "ticket_operation",
                column: "employee_id",
                principalTable: "employee",
                principalColumn: "employee_id");

            migrationBuilder.AddForeignKey(
                name: "FK_window_employee_employee_id",
                table: "window",
                column: "employee_id",
                principalTable: "employee",
                principalColumn: "employee_id");
        }
    }
}
