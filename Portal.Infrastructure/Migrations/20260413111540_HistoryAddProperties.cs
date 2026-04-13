using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HistoryAddProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecipienUserId",
                table: "HistoryEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResponsibleUserId",
                table: "HistoryEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderUserId",
                table: "HistoryEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserView",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserView_UserDepartments_UserDepartmentId",
                        column: x => x.UserDepartmentId,
                        principalTable: "UserDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserView_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_RecipienUserId",
                table: "HistoryEntries",
                column: "RecipienUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_ResponsibleUserId",
                table: "HistoryEntries",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_SenderUserId",
                table: "HistoryEntries",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_UserWarehouseId",
                table: "HistoryEntries",
                column: "UserWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_WarehouseId",
                table: "HistoryEntries",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserView_UserDepartmentId",
                table: "UserView",
                column: "UserDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserView_UserRoleId",
                table: "UserView",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntries_MainWarehouses_WarehouseId",
                table: "HistoryEntries",
                column: "WarehouseId",
                principalTable: "MainWarehouses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntries_UserView_RecipienUserId",
                table: "HistoryEntries",
                column: "RecipienUserId",
                principalTable: "UserView",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntries_UserView_ResponsibleUserId",
                table: "HistoryEntries",
                column: "ResponsibleUserId",
                principalTable: "UserView",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntries_UserView_SenderUserId",
                table: "HistoryEntries",
                column: "SenderUserId",
                principalTable: "UserView",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntries_UserWarehouses_UserWarehouseId",
                table: "HistoryEntries",
                column: "UserWarehouseId",
                principalTable: "UserWarehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntries_MainWarehouses_WarehouseId",
                table: "HistoryEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntries_UserView_RecipienUserId",
                table: "HistoryEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntries_UserView_ResponsibleUserId",
                table: "HistoryEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntries_UserView_SenderUserId",
                table: "HistoryEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntries_UserWarehouses_UserWarehouseId",
                table: "HistoryEntries");

            migrationBuilder.DropTable(
                name: "UserView");

            migrationBuilder.DropIndex(
                name: "IX_HistoryEntries_RecipienUserId",
                table: "HistoryEntries");

            migrationBuilder.DropIndex(
                name: "IX_HistoryEntries_ResponsibleUserId",
                table: "HistoryEntries");

            migrationBuilder.DropIndex(
                name: "IX_HistoryEntries_SenderUserId",
                table: "HistoryEntries");

            migrationBuilder.DropIndex(
                name: "IX_HistoryEntries_UserWarehouseId",
                table: "HistoryEntries");

            migrationBuilder.DropIndex(
                name: "IX_HistoryEntries_WarehouseId",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "RecipienUserId",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserId",
                table: "HistoryEntries");

            migrationBuilder.DropColumn(
                name: "SenderUserId",
                table: "HistoryEntries");
        }
    }
}
