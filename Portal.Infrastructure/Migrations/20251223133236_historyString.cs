using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class historyString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainWarehouse",
                table: "HistoryEntries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipientString",
                table: "HistoryEntries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responsible",
                table: "HistoryEntries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderString",
                table: "HistoryEntries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserWarehouse",
                table: "HistoryEntries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainWarehouse",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "RecipientString",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "Responsible",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "SenderString",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "UserWarehouse",
                table: "HistoryEntries");
        }
    }
}
