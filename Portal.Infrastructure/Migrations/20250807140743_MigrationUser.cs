using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserTokenId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d7fa6b79-cf3b-442e-37e6-08ddd5a32cac"),
                column: "UserTokenId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f29533dd-9a3c-4889-d468-08ddd5a47b7a"),
                column: "UserTokenId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTokenId",
                table: "Users",
                column: "UserTokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTokens_UserTokenId",
                table: "Users",
                column: "UserTokenId",
                principalTable: "UserTokens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTokens_UserTokenId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTokenId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTokenId",
                table: "Users");
        }
    }
}
