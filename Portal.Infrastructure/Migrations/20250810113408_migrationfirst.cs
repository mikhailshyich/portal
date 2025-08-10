using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migrationfirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Hardwares",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_UserId",
                table: "Hardwares",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hardwares_Users_UserId",
                table: "Hardwares",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hardwares_Users_UserId",
                table: "Hardwares");

            migrationBuilder.DropIndex(
                name: "IX_Hardwares_UserId",
                table: "Hardwares");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hardwares");
        }
    }
}
