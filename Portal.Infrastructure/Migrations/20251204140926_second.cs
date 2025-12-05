using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Portal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriesHardware",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ShortTitle = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ShortTitle = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsExternalSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PublicTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersHardware",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HardwareId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersHardware", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                name: "Hardwares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MainWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryHardwareId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentExternalSystemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MarkCode = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    InventoryNumberExternalSystem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TTN = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DateTimeAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileNameImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CombinedInvNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                name: "MarkCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HardwareId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MarkCodeNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Used = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarkCodes_Hardwares_HardwareId",
                        column: x => x.HardwareId,
                        principalTable: "Hardwares",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoryEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateTimeChanges = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "UserWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWarehouses_Users_UserId",
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
                table: "MainWarehouses",
                columns: new[] { "Id", "Description", "Title", "UserDepartmentId" },
                values: new object[] { new Guid("c9006a11-ac35-4df9-0c93-08ddd89f84c8"), "", "Основной склад", new Guid("d7fa6b79-cf7b-442e-32e6-08ddd5a32cac") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "LastName", "PasswordHash", "Patronymic", "Specialization", "UserDepartmentId", "UserRoleId", "UserTokenId", "Username" },
                values: new object[,]
                {
                    { new Guid("d7fa6b79-cf3b-442e-37e6-08ddd5a32cac"), "user@user.by", "", true, "Пользователь", "AQAAAAIAAYagAAAAEOVSg/5PKFU0eFXRm9R6j5GvdEhsxlIymU+I51+5Y/+gQX+c7AHCeu/ZT5ByOLFk7w==", "", "", new Guid("d7fa6b79-cf7b-442e-32e6-08ddd5a32cac"), new Guid("d7fa6b79-cf7b-442e-37e6-08ddd5a32cac"), null, "user" },
                    { new Guid("f29533dd-9a3c-4889-d468-08ddd5a47b7a"), "admin@admin.by", "", true, "Администратор", "AQAAAAIAAYagAAAAEEnHKp66I1iY4a6WutPESx3dIQF0V/ITse74a7euQmBiSo8E516lhTSbFbEqJVAKQw==", "", "", new Guid("d7fa6b79-cf7b-442e-32e6-08ddd5a32cac"), new Guid("f29533dd-9a3c-4899-d468-08ddd5a47b7a"), null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserWarehouses",
                columns: new[] { "Id", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("0588a0e8-fdc8-4310-1850-08ddd8a42ead"), "Склад пользователя по умолчанию", new Guid("f29533dd-9a3c-4889-d468-08ddd5a47b7a") },
                    { new Guid("0588a0e8-fdc8-4317-1850-08ddd8a42ead"), "Склад пользователя по умолчанию", new Guid("d7fa6b79-cf3b-442e-37e6-08ddd5a32cac") }
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
                name: "IX_Hardwares_UserId",
                table: "Hardwares",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_UserWarehouseId",
                table: "Hardwares",
                column: "UserWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_UserId",
                table: "HistoryEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainWarehouses_UserDepartmentId",
                table: "MainWarehouses",
                column: "UserDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkCodes_HardwareId",
                table: "MarkCodes",
                column: "HardwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserDepartmentId",
                table: "Users",
                column: "UserDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTokenId",
                table: "Users",
                column: "UserTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWarehouses_UserId",
                table: "UserWarehouses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hardwares_UserWarehouses_UserWarehouseId",
                table: "Hardwares",
                column: "UserWarehouseId",
                principalTable: "UserWarehouses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hardwares_Users_UserId",
                table: "Hardwares",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntries_Users_UserId",
                table: "HistoryEntries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens");

            migrationBuilder.DropTable(
                name: "HistoryEntries");

            migrationBuilder.DropTable(
                name: "MarkCodes");

            migrationBuilder.DropTable(
                name: "UsersHardware");

            migrationBuilder.DropTable(
                name: "Hardwares");

            migrationBuilder.DropTable(
                name: "CategoriesHardware");

            migrationBuilder.DropTable(
                name: "DocumentsExternalSystem");

            migrationBuilder.DropTable(
                name: "MainWarehouses");

            migrationBuilder.DropTable(
                name: "UserWarehouses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserDepartments");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");
        }
    }
}
