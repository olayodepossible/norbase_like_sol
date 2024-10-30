public class Article
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int LikesCount { get; set; }
    public List<Like> Likes { get; set; }
}


