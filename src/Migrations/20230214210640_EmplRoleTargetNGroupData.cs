using Microsoft.EntityFrameworkCore.Migrations;

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
            //Заполняем справочник group
            migrationBuilder.Sql(@"INSERT INTO ""group"" (group_id,name,group_link_id)VALUES(1,'Овощи',null),(2,'Мясо',null),(3,'Фрукты',null),(4,'Сладости',null),
                                (5,'Свежие',1),(6,'Маринованные',1),(7,'Свежее',2),(8,'Вяленое',2);");
            //Заполняем справочник target
            migrationBuilder.Sql(@"INSERT INTO target (name,prefix,group_link_id)VALUES('Помидоры','П',5),('Огурцы','О',6),
                                ('Конина','К',8),('Шоколад','Ш',4);");
            //Заполняем справочник window
            migrationBuilder.Sql(@"INSERT INTO ""window"" (number)VALUES('1'),('2'),('3'),('4');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "employee");
        }
    }
}
