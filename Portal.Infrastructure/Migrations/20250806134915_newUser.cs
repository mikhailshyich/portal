using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeExpiredJWT",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "JWTToken",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RefToken",
                table: "UserTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "DateTimeExpiredRef",
                table: "UserTokens",
                newName: "DateTimeExpiredToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "UserTokens",
                newName: "RefToken");

            migrationBuilder.RenameColumn(
                name: "DateTimeExpiredToken",
                table: "UserTokens",
                newName: "DateTimeExpiredRef");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeExpiredJWT",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "JWTToken",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
