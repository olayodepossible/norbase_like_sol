public partial class AddLikesFeature : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Likes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ArticleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Likes", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Likes_ArticleId_UserId",
            table: "Likes",
            columns: new[] { "ArticleId", "UserId" },
            unique: true);
    }
}