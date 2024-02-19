using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Authentication.Migrations
{
    /// <inheritdoc />
    public partial class addv7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_tbl_Role_tbl_RoleId",
                table: "User_tbl");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User_tbl",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "User_tbl",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_tbl_UserName",
                table: "User_tbl",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_tbl_Role_tbl_RoleId",
                table: "User_tbl",
                column: "RoleId",
                principalTable: "Role_tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_tbl_Role_tbl_RoleId",
                table: "User_tbl");

            migrationBuilder.DropIndex(
                name: "IX_User_tbl_UserName",
                table: "User_tbl");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User_tbl",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "User_tbl",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_User_tbl_Role_tbl_RoleId",
                table: "User_tbl",
                column: "RoleId",
                principalTable: "Role_tbl",
                principalColumn: "Id");
        }
    }
}
