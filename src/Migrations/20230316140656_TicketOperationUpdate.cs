using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bbqueue.Migrations
{
    /// <inheritdoc />
    public partial class TicketOperationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "updated",
                table: "ticket_operation",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updated",
                table: "ticket_operation");
        }
    }
}
