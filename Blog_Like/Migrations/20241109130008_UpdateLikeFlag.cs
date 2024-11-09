using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Like.Migrations
{
    public partial class UpdateLikeFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "hasLiked",
                table: "Likes",
                newName: "HasLiked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasLiked",
                table: "Likes",
                newName: "hasLiked");
        }
    }
}
