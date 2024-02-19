using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Authentication.Migrations
{
    /// <inheritdoc />
    public partial class addv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "confirmEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodeActive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_confirmEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_confirmEmails_User_tbl_UserId",
                        column: x => x.UserId,
                        principalTable: "User_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post_Type_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post_Type_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfLikes = table.Column<int>(type: "int", nullable: false),
                    NumberOfComments = table.Column<int>(type: "int", nullable: false),
                    View = table.Column<int>(type: "int", nullable: false),
                    PinedPost = table.Column<bool>(type: "bit", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_tbl_Post_Type_tbl_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "Post_Type_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_tbl_User_tbl_UserId",
                        column: x => x.UserId,
                        principalTable: "User_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_tbl_Post_tbl_PostId",
                        column: x => x.PostId,
                        principalTable: "Post_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_tbl_User_tbl_UserId",
                        column: x => x.UserId,
                        principalTable: "User_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_tbl_PostId",
                table: "Comment_tbl",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_tbl_UserId",
                table: "Comment_tbl",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_confirmEmails_UserId",
                table: "confirmEmails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_tbl_PostTypeId",
                table: "Post_tbl",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_tbl_UserId",
                table: "Post_tbl",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment_tbl");

            migrationBuilder.DropTable(
                name: "confirmEmails");

            migrationBuilder.DropTable(
                name: "Post_tbl");

            migrationBuilder.DropTable(
                name: "Post_Type_tbl");
        }
    }
}
