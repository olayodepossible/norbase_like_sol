using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Like.Migrations
{
    public partial class AddLikeFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "hasLiked",
                table: "Likes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasLiked",
                table: "Likes");
        }
    }
}
