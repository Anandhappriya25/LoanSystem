using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanSystem.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_LoanId",
                table: "LoanDetails",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_CustomerId",
                table: "Loan",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LoanTypeId",
                table: "Loan",
                column: "LoanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_RoleId",
                table: "Customer",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Roles_RoleId",
                table: "Customer",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Customer_CustomerId",
                table: "Loan",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_LoanType_LoanTypeId",
                table: "Loan",
                column: "LoanTypeId",
                principalTable: "LoanType",
                principalColumn: "LoanTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDetails_Loan_LoanId",
                table: "LoanDetails",
                column: "LoanId",
                principalTable: "Loan",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Roles_RoleId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Customer_CustomerId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_LoanType_LoanTypeId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanDetails_Loan_LoanId",
                table: "LoanDetails");

            migrationBuilder.DropIndex(
                name: "IX_LoanDetails_LoanId",
                table: "LoanDetails");

            migrationBuilder.DropIndex(
                name: "IX_Loan_CustomerId",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_LoanTypeId",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Customer_RoleId",
                table: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "MobileNumber",
                table: "Customer",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
