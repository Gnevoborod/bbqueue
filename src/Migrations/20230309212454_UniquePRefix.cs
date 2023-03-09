using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace bbqueue.Migrations
{
    /// <inheritdoc />
    public partial class UniquePRefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_group_group_link_id",
                table: "group");

            migrationBuilder.DropForeignKey(
                name: "FK_target_group_group_link_id",
                table: "target");

            migrationBuilder.DropForeignKey(
                name: "FK_window_target_target_target_id",
                table: "window_target");

            migrationBuilder.DropPrimaryKey(
                name: "PK_target",
                table: "target");

            migrationBuilder.DropPrimaryKey(
                name: "PK_group",
                table: "group");

            migrationBuilder.RenameTable(
                name: "target",
                newName: "Target");

            migrationBuilder.RenameTable(
                name: "group",
                newName: "Group");

            migrationBuilder.RenameColumn(
                name: "prefix",
                table: "Target",
                newName: "Prefix");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Target",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Target",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "group_link_id",
                table: "Target",
                newName: "GroupLinkId");

            migrationBuilder.RenameColumn(
                name: "target_id",
                table: "Target",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_target_group_link_id",
                table: "Target",
                newName: "IX_Target_GroupLinkId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Group",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Group",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "group_link_id",
                table: "Group",
                newName: "GroupLinkId");

            migrationBuilder.RenameColumn(
                name: "group_id",
                table: "Group",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_group_group_link_id",
                table: "Group",
                newName: "IX_Group_GroupLinkId");

            migrationBuilder.AddColumn<long>(
                name: "target_id",
                table: "ticket_operation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Target",
                table: "Target",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_ticket_operation_target_id",
                table: "ticket_operation",
                column: "target_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_amount_prefix",
                table: "ticket_amount",
                column: "prefix",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_group_group_link_id",
                table: "group",
                column: "group_link_id");

            migrationBuilder.CreateIndex(
                name: "IX_target_group_link_id",
                table: "target",
                column: "group_link_id");

            migrationBuilder.CreateIndex(
                name: "IX_target_prefix",
                table: "target",
                column: "prefix",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Group_GroupLinkId",
                table: "Group",
                column: "GroupLinkId",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Target_Group_GroupLinkId",
                table: "Target",
                column: "GroupLinkId",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_operation_Target_target_id",
                table: "ticket_operation",
                column: "target_id",
                principalTable: "Target",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_window_target_target_target_id",
                table: "window_target",
                column: "target_id",
                principalTable: "target",
                principalColumn: "target_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Group_GroupLinkId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Target_Group_GroupLinkId",
                table: "Target");

            migrationBuilder.DropForeignKey(
                name: "FK_ticket_operation_Target_target_id",
                table: "ticket_operation");

            migrationBuilder.DropForeignKey(
                name: "FK_window_target_target_target_id",
                table: "window_target");

            migrationBuilder.DropTable(
                name: "target");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropIndex(
                name: "IX_ticket_operation_target_id",
                table: "ticket_operation");

            migrationBuilder.DropIndex(
                name: "IX_ticket_amount_prefix",
                table: "ticket_amount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Target",
                table: "Target");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "target_id",
                table: "ticket_operation");

            migrationBuilder.RenameTable(
                name: "Target",
                newName: "target");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "group");

            migrationBuilder.RenameColumn(
                name: "Prefix",
                table: "target",
                newName: "prefix");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "target",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "target",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "GroupLinkId",
                table: "target",
                newName: "group_link_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "target",
                newName: "target_id");

            migrationBuilder.RenameIndex(
                name: "IX_Target_GroupLinkId",
                table: "target",
                newName: "IX_target_group_link_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "group",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "group",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "GroupLinkId",
                table: "group",
                newName: "group_link_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "group",
                newName: "group_id");

            migrationBuilder.RenameIndex(
                name: "IX_Group_GroupLinkId",
                table: "group",
                newName: "IX_group_group_link_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_target",
                table: "target",
                column: "target_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_group",
                table: "group",
                column: "group_id");

            migrationBuilder.AddForeignKey(
                name: "FK_group_group_group_link_id",
                table: "group",
                column: "group_link_id",
                principalTable: "group",
                principalColumn: "group_id");

            migrationBuilder.AddForeignKey(
                name: "FK_target_group_group_link_id",
                table: "target",
                column: "group_link_id",
                principalTable: "group",
                principalColumn: "group_id");

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
