namespace MyBlog.Model.Domains
{
    public class Like
    {
        public int Id { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
