using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noghte.Infrastructure.Migrations
{
    public partial class Add_UserLikedListIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikedPost",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PostId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedPost_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LikedPost_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedPost_PostId",
                table: "LikedPost",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedPost_UserId",
                table: "LikedPost",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedPost");
        }
    }
}
