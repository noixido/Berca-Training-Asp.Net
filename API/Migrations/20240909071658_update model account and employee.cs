using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updatemodelaccountandemployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_Dept_ID",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Dept_ID",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Dept_ID",
                table: "Employees",
                column: "Dept_ID",
                principalTable: "Departments",
                principalColumn: "Dept_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_Dept_ID",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Dept_ID",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Dept_ID",
                table: "Employees",
                column: "Dept_ID",
                principalTable: "Departments",
                principalColumn: "Dept_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
