using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updateAccountModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Employees_Account_ID",
                table: "Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Account_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_Account_ID",
                table: "Accounts",
                column: "Account_ID",
                principalTable: "Employees",
                principalColumn: "Employee_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_Account_ID",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Account_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Employees_Account_ID",
                table: "Account",
                column: "Account_ID",
                principalTable: "Employees",
                principalColumn: "Employee_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
