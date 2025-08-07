using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriesHardware",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesHardware", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsExternalSystem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsExternalSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainWarehouses_UserDepartments_UserDepartmentId",
                        column: x => x.UserDepartmentId,
                        principalTable: "UserDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserDepartments_UserDepartmentId",
                        column: x => x.UserDepartmentId,
                        principalTable: "UserDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hardwares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MainWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryHardwareId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentExternalSystemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryNumberExternalSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TTN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileNameImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardwares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hardwares_CategoriesHardware_CategoryHardwareId",
                        column: x => x.CategoryHardwareId,
                        principalTable: "CategoriesHardware",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hardwares_DocumentsExternalSystem_DocumentExternalSystemId",
                        column: x => x.DocumentExternalSystemId,
                        principalTable: "DocumentsExternalSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hardwares_MainWarehouses_MainWarehouseId",
                        column: x => x.MainWarehouseId,
                        principalTable: "MainWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeExpiredToken = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserDepartments",
                columns: new[] { "Id", "ShortTitle", "Title" },
                values: new object[] { new Guid("d7fa6b79-cf7b-442e-32e6-08ddd5a32cac"), "", "Отдел по умолчанию" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "PublicTitle", "Title" },
                values: new object[,]
                {
                    { new Guid("d7fa6b79-cf7b-442e-37e6-08ddd5a32cac"), "Пользователь", "user" },
                    { new Guid("f29533dd-9a3c-4899-d468-08ddd5a47b7a"), "Администратор", "admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "UserDepartmentId", "UserRoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("d7fa6b79-cf3b-442e-37e6-08ddd5a32cac"), "user@user.by", "AQAAAAIAAYagAAAAEOVSg/5PKFU0eFXRm9R6j5GvdEhsxlIymU+I51+5Y/+gQX+c7AHCeu/ZT5ByOLFk7w==", new Guid("d7fa6b79-cf7b-442e-32e6-08ddd5a32cac"), new Guid("d7fa6b79-cf7b-442e-37e6-08ddd5a32cac"), "user" },
                    { new Guid("f29533dd-9a3c-4889-d468-08ddd5a47b7a"), "admin@admin.by", "AQAAAAIAAYagAAAAEEnHKp66I1iY4a6WutPESx3dIQF0V/ITse74a7euQmBiSo8E516lhTSbFbEqJVAKQw==", new Guid("d7fa6b79-cf7b-442e-32e6-08ddd5a32cac"), new Guid("f29533dd-9a3c-4899-d468-08ddd5a47b7a"), "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_CategoryHardwareId",
                table: "Hardwares",
                column: "CategoryHardwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_DocumentExternalSystemId",
                table: "Hardwares",
                column: "DocumentExternalSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_MainWarehouseId",
                table: "Hardwares",
                column: "MainWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_MainWarehouses_UserDepartmentId",
                table: "MainWarehouses",
                column: "UserDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserDepartmentId",
                table: "Users",
                column: "UserDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Hardwares");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "CategoriesHardware");

            migrationBuilder.DropTable(
                name: "DocumentsExternalSystem");

            migrationBuilder.DropTable(
                name: "MainWarehouses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserDepartments");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
