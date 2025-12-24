using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class history : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntries_Users_UserId",
                table: "HistoryEntries");

            migrationBuilder.DropIndex(
                name: "IX_HistoryEntries_UserId",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HistoryEntries");

            migrationBuilder.AlterColumn<string>(
                name: "InventoryNumberExternalSystem",
                table: "Hardwares",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "HistoryEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InventoryNumberExternalSystem",
                table: "Hardwares",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_UserId",
                table: "HistoryEntries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntries_Users_UserId",
                table: "HistoryEntries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
