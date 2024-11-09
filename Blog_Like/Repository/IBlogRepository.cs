using MyBlog.Model.Domains;

namespace Blog_Like.Repository
{
    public interface IBlogRepository
    {
        Task<Article?> GetArticleById(int articleId);
        Task<User?> GetUserById(int userId);
        Task<int> GetLikeCountForArticle(int articleId);
        Task<Like?> GetLikeByUserIdAndArticleId(int userId, int articleId);
        Task<Like> CreateLikeAsync(Like like);
        Task<Like?> UpdateLikeAsync(Like existingLike);
    }
}
