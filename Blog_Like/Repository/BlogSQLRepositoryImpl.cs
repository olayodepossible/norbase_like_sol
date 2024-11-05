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
        public async Task<Article?> GetArticleById(Guid id)
        {
            return await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
