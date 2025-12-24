using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class historyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderString",
                table: "HistoryEntries",
                newName: "Sender");

            migrationBuilder.RenameColumn(
                name: "RecipientString",
                table: "HistoryEntries",
                newName: "Recipient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "HistoryEntries",
                newName: "SenderString");

            migrationBuilder.RenameColumn(
                name: "Recipient",
                table: "HistoryEntries",
                newName: "RecipientString");
        }
    }
}
