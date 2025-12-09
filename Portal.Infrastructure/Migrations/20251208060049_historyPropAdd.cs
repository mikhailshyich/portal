using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class historyPropAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationType",
                table: "HistoryEntries");

            migrationBuilder.AddColumn<Guid>(
                name: "HardwareId",
                table: "HistoryEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecipientId",
                table: "HistoryEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "HistoryEntries",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HardwareId",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "HistoryEntries");

            migrationBuilder.AddColumn<string>(
                name: "OperationType",
                table: "HistoryEntries",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
