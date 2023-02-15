using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;

#nullable disable

namespace bbqueue.Migrations
{
    /// <inheritdoc />
    public partial class EmplRoleTargetNGroupData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "employee",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AlterColumn<int>(
                name: "employee_id",
                table: "window",
                type: "integer",
                nullable: true,
                defaultValue: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "employee");

            migrationBuilder.AlterColumn<int>(
                name: "employee_id",
                table: "window",
                type: "integer",
                nullable: false,
                defaultValue: null);

        }
    }
}
