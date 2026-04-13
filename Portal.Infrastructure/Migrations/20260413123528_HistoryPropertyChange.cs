using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HistoryPropertyChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntries_MainWarehouses_WarehouseId",
                table: "HistoryEntries");

            migrationBuilder.DropIndex(
                name: "IX_HistoryEntries_WarehouseId",
                table: "HistoryEntries");

            migrationBuilder.RenameColumn(
                name: "UserWarehouse",
                table: "HistoryEntries",
                newName: "UserWarehouseString");

            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "HistoryEntries",
                newName: "SenderString");

            migrationBuilder.RenameColumn(
                name: "Responsible",
                table: "HistoryEntries",
                newName: "ResponsibleString");

            migrationBuilder.RenameColumn(
                name: "Recipient",
                table: "HistoryEntries",
                newName: "RecipientString");

            migrationBuilder.RenameColumn(
                name: "MainWarehouse",
                table: "HistoryEntries",
                newName: "MainWarehouseString");

            migrationBuilder.AddColumn<Guid>(
                name: "MainWarehouseId",
                table: "HistoryEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_MainWarehouseId",
                table: "HistoryEntries",
                column: "MainWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntries_MainWarehouses_MainWarehouseId",
                table: "HistoryEntries",
                column: "MainWarehouseId",
                principalTable: "MainWarehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntries_MainWarehouses_MainWarehouseId",
                table: "HistoryEntries");

            migrationBuilder.DropIndex(
                name: "IX_HistoryEntries_MainWarehouseId",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "MainWarehouseId",
                table: "HistoryEntries");

            migrationBuilder.RenameColumn(
                name: "UserWarehouseString",
                table: "HistoryEntries",
                newName: "UserWarehouse");

            migrationBuilder.RenameColumn(
                name: "SenderString",
                table: "HistoryEntries",
                newName: "Sender");

            migrationBuilder.RenameColumn(
                name: "ResponsibleString",
                table: "HistoryEntries",
                newName: "Responsible");

            migrationBuilder.RenameColumn(
                name: "RecipientString",
                table: "HistoryEntries",
                newName: "Recipient");

            migrationBuilder.RenameColumn(
                name: "MainWarehouseString",
                table: "HistoryEntries",
                newName: "MainWarehouse");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_WarehouseId",
                table: "HistoryEntries",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntries_MainWarehouses_WarehouseId",
                table: "HistoryEntries",
                column: "WarehouseId",
                principalTable: "MainWarehouses",
                principalColumn: "Id");
        }
    }
}
