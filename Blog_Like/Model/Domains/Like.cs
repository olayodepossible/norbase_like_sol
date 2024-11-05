namespace MyBlog.Model.Domains
{
    public class Like
    {
        public int Id { get; set; }
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
