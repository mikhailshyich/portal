using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migrationfirst0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersHardware_Users_UserId",
                table: "UsersHardware");

            migrationBuilder.DropIndex(
                name: "IX_UsersHardware_UserId",
                table: "UsersHardware");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersHardware_UserId",
                table: "UsersHardware",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersHardware_Users_UserId",
                table: "UsersHardware",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
