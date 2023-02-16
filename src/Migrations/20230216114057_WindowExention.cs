using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bbqueue.Migrations
{
    /// <inheritdoc />
    public partial class WindowExention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_window_employee_employee_id",
                table: "window");

            migrationBuilder.AlterColumn<long>(
                name: "employee_id",
                table: "window",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "window_work_state",
                table: "window",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_window_employee_employee_id",
                table: "window",
                column: "employee_id",
                principalTable: "employee",
                principalColumn: "employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_window_employee_employee_id",
                table: "window");

            migrationBuilder.DropColumn(
                name: "window_work_state",
                table: "window");

            migrationBuilder.AlterColumn<long>(
                name: "employee_id",
                table: "window",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_window_employee_employee_id",
                table: "window",
                column: "employee_id",
                principalTable: "employee",
                principalColumn: "employee_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
