using Microsoft.EntityFrameworkCore.Migrations;

namespace LDInsurance.Migrations
{
    public partial class update8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsuranceRegistrationID",
                table: "TransactionHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_InsuranceRegistrationID",
                table: "TransactionHistory",
                column: "InsuranceRegistrationID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_InsuranceRegistrations_InsuranceRegistrationID",
                table: "TransactionHistory",
                column: "InsuranceRegistrationID",
                principalTable: "InsuranceRegistrations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_InsuranceRegistrations_InsuranceRegistrationID",
                table: "TransactionHistory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionHistory_InsuranceRegistrationID",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "InsuranceRegistrationID",
                table: "TransactionHistory");
        }
    }
}
