using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserWarehouse1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserWarehouseId",
                table: "Hardwares",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_UserWarehouseId",
                table: "Hardwares",
                column: "UserWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hardwares_UserWarehouses_UserWarehouseId",
                table: "Hardwares",
                column: "UserWarehouseId",
                principalTable: "UserWarehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hardwares_UserWarehouses_UserWarehouseId",
                table: "Hardwares");

            migrationBuilder.DropIndex(
                name: "IX_Hardwares_UserWarehouseId",
                table: "Hardwares");

            migrationBuilder.DropColumn(
                name: "UserWarehouseId",
                table: "Hardwares");
        }
    }
}
