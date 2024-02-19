using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Authentication.Migrations
{
    /// <inheritdoc />
    public partial class addv6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_tbl_Post_tbl_PostId",
                table: "Comment_tbl");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_tbl_User_tbl_UserId",
                table: "Comment_tbl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment_tbl",
                table: "Comment_tbl");

            migrationBuilder.RenameTable(
                name: "Comment_tbl",
                newName: "Comments_tbl");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_tbl_UserId",
                table: "Comments_tbl",
                newName: "IX_Comments_tbl_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_tbl_PostId",
                table: "Comments_tbl",
                newName: "IX_Comments_tbl_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments_tbl",
                table: "Comments_tbl",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_tbl_Post_tbl_PostId",
                table: "Comments_tbl",
                column: "PostId",
                principalTable: "Post_tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_tbl_User_tbl_UserId",
                table: "Comments_tbl",
                column: "UserId",
                principalTable: "User_tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_tbl_Post_tbl_PostId",
                table: "Comments_tbl");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_tbl_User_tbl_UserId",
                table: "Comments_tbl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments_tbl",
                table: "Comments_tbl");

            migrationBuilder.RenameTable(
                name: "Comments_tbl",
                newName: "Comment_tbl");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_tbl_UserId",
                table: "Comment_tbl",
                newName: "IX_Comment_tbl_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_tbl_PostId",
                table: "Comment_tbl",
                newName: "IX_Comment_tbl_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment_tbl",
                table: "Comment_tbl",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_tbl_Post_tbl_PostId",
                table: "Comment_tbl",
                column: "PostId",
                principalTable: "Post_tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_tbl_User_tbl_UserId",
                table: "Comment_tbl",
                column: "UserId",
                principalTable: "User_tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
