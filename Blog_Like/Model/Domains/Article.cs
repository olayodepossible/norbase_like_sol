using System.Text.Json;

namespace MyBlog.Model.Domains
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArticleBody { get; set; }
        public int LikesCount { get; set; }
        public List<Like> Likes { get; set; }
        public List<BlogComment> comments { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
