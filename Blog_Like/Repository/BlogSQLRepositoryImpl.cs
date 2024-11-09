using Microsoft.EntityFrameworkCore;
using MyBlog.Db;
using MyBlog.Model.Domains;

namespace Blog_Like.Repository
{
    public class BlogSQLRepositoryImpl : IBlogRepository
    {
        private readonly BlogDbContext dbContext;

        public BlogSQLRepositoryImpl(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Article?> GetArticleById(int id)
        {
            return await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Like?> GetLikeByUserIdAndArticleId(int userId, int articleId)
        {
           return await dbContext.Likes.FirstOrDefaultAsync(l => l.ArticleId == articleId && l.UserId == userId);
        }

        public async Task<int> GetLikeCountForArticle(int articleId)
        {
            return await dbContext.Likes
                .CountAsync(like => like.ArticleId == articleId && like.HasLiked == true);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Like> CreateLikeAsync(Like like)
        {
            await dbContext.Likes.AddAsync(like);
            await dbContext.SaveChangesAsync();
            return like;
        }

        public async Task<Like?> UpdateLikeToggleAsync(Like existingLike)
        {
            existingLike.HasLiked = !existingLike.HasLiked;
            existingLike.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return existingLike;
        }
    }
}
