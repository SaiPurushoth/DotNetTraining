using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MutualFundManagement.Migrations
{
    public partial class ThirdDbUpdation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_MutualFundBanks_mutualFundFundId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_mutualFundFundId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "mutualFundFundId",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "mutualFundFundId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_mutualFundFundId",
                table: "Customers",
                column: "mutualFundFundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_MutualFundBanks_mutualFundFundId",
                table: "Customers",
                column: "mutualFundFundId",
                principalTable: "MutualFundBanks",
                principalColumn: "FundId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
