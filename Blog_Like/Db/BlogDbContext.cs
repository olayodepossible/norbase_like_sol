using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyBlog.Model.Domains;

namespace MyBlog.Db
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> dbContextOptions) : base(dbContextOptions)
        {


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<BlogComment> Comments { get; set; }


    }
}
