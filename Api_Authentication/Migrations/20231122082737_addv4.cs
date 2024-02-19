using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Authentication.Migrations
{
    /// <inheritdoc />
    public partial class addv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_tbl_Role_tbl_RoleId",
                table: "User_tbl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Role_tbl",
                newName: "RoleName");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "User_tbl",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "RefreshToken_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_tbl_User_tbl_UserId",
                        column: x => x.UserId,
                        principalTable: "User_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_tbl_UserId",
                table: "RefreshToken_tbl",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_tbl_Role_tbl_RoleId",
                table: "User_tbl",
                column: "RoleId",
                principalTable: "Role_tbl",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_tbl_Role_tbl_RoleId",
                table: "User_tbl");

            migrationBuilder.DropTable(
                name: "RefreshToken_tbl");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Role_tbl",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "User_tbl",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_tbl_Role_tbl_RoleId",
                table: "User_tbl",
                column: "RoleId",
                principalTable: "Role_tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
