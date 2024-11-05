using MyBlog.Model.Domains;

namespace Blog_Like.Repository
{
    public interface IBlogRepository
    {
        Task<Article?> GetArticleById(Guid id);
        Task<User?> GetUserById(Guid id);
    }
}
