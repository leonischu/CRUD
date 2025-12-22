using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class creatingfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "RolePrivileges",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivileges_RoleId",
                table: "RolePrivileges",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "Fk_RolePrivileges_Roles",
                table: "RolePrivileges",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_RolePrivileges_Roles",
                table: "RolePrivileges");

            migrationBuilder.DropIndex(
                name: "IX_RolePrivileges_RoleId",
                table: "RolePrivileges");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "RolePrivileges",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
