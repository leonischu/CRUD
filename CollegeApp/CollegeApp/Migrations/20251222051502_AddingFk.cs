using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "Fk_Users_UserTypes",
                table: "User",
                column: "UserTypeId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Users_UserTypes",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserTypeId",
                table: "User");
        }
    }
}
