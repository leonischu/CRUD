using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingRoleAndRolePrivilegeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolePrivileges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolePrivilegeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", maxLength: 500, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePrivileges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", maxLength: 500, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePrivileges");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
