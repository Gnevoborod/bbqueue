using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace bbqueue.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employeeid = table.Column<long>(name: "employee_id", type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    externalsystemid = table.Column<string>(name: "external_system_id", type: "character varying(16)", maxLength: 16, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employeeid);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    groupid = table.Column<long>(name: "group_id", type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    grouplinkid = table.Column<long>(name: "group_link_id", type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.groupid);
                    table.ForeignKey(
                        name: "FK_group_group_group_link_id",
                        column: x => x.grouplinkid,
                        principalTable: "group",
                        principalColumn: "group_id");
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    ticketid = table.Column<long>(name: "ticket_id", type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    closed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.ticketid);
                });

            migrationBuilder.CreateTable(
                name: "ticket_amount",
                columns: table => new
                {
                    ticketamountid = table.Column<long>(name: "ticket_amount_id", type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    prefix = table.Column<char>(type: "character(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_amount", x => x.ticketamountid);
                });

            migrationBuilder.CreateTable(
                name: "window",
                columns: table => new
                {
                    windowid = table.Column<long>(name: "window_id", type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    employeeid = table.Column<long>(name: "employee_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_window", x => x.windowid);
                    table.ForeignKey(
                        name: "FK_window_employee_employee_id",
                        column: x => x.employeeid,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "target",
                columns: table => new
                {
                    targetid = table.Column<long>(name: "target_id", type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    prefix = table.Column<char>(type: "character(1)", nullable: false),
                    grouplinkid = table.Column<long>(name: "group_link_id", type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_target", x => x.targetid);
                    table.ForeignKey(
                        name: "FK_target_group_group_link_id",
                        column: x => x.grouplinkid,
                        principalTable: "group",
                        principalColumn: "group_id");
                });

            migrationBuilder.CreateTable(
                name: "ticket_operation",
                columns: table => new
                {
                    ticketopearationid = table.Column<long>(name: "ticket_opearation_id", type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticketid = table.Column<long>(name: "ticket_id", type: "bigint", nullable: false),
                    windowid = table.Column<long>(name: "window_id", type: "bigint", nullable: true),
                    employeeid = table.Column<long>(name: "employee_id", type: "bigint", nullable: true),
                    state = table.Column<int>(type: "integer", nullable: false),
                    processed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_operation", x => x.ticketopearationid);
                    table.ForeignKey(
                        name: "FK_ticket_operation_employee_employee_id",
                        column: x => x.employeeid,
                        principalTable: "employee",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "FK_ticket_operation_ticket_ticket_id",
                        column: x => x.ticketid,
                        principalTable: "ticket",
                        principalColumn: "ticket_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_operation_window_window_id",
                        column: x => x.windowid,
                        principalTable: "window",
                        principalColumn: "window_id");
                });

            migrationBuilder.CreateTable(
                name: "window_target",
                columns: table => new
                {
                    windowtargetid = table.Column<long>(name: "window_target_id", type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    windowid = table.Column<long>(name: "window_id", type: "bigint", nullable: false),
                    targetid = table.Column<long>(name: "target_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_window_target", x => x.windowtargetid);
                    table.ForeignKey(
                        name: "FK_window_target_target_target_id",
                        column: x => x.targetid,
                        principalTable: "target",
                        principalColumn: "target_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_window_target_window_window_id",
                        column: x => x.windowid,
                        principalTable: "window",
                        principalColumn: "window_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_group_group_link_id",
                table: "group",
                column: "group_link_id");

            migrationBuilder.CreateIndex(
                name: "IX_target_group_link_id",
                table: "target",
                column: "group_link_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_operation_employee_id",
                table: "ticket_operation",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_operation_ticket_id",
                table: "ticket_operation",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_operation_window_id",
                table: "ticket_operation",
                column: "window_id");

            migrationBuilder.CreateIndex(
                name: "IX_window_employee_id",
                table: "window",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_window_target_target_id",
                table: "window_target",
                column: "target_id");

            migrationBuilder.CreateIndex(
                name: "IX_window_target_window_id",
                table: "window_target",
                column: "window_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket_amount");

            migrationBuilder.DropTable(
                name: "ticket_operation");

            migrationBuilder.DropTable(
                name: "window_target");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "target");

            migrationBuilder.DropTable(
                name: "window");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "employee");
        }
    }
}
