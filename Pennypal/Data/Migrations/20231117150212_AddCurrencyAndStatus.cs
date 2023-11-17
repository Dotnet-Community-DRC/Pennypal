using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pennypal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Reports",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "ReportName",
                table: "Reports",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ReportDate",
                table: "Reports",
                newName: "Amount");

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Expenses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Expenses",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Reports",
                newName: "ReportName");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Reports",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Reports",
                newName: "ReportDate");
        }
    }
}
