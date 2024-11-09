using Blog_Like.Repository;
using Microsoft.EntityFrameworkCore;
using MyBlog.Db;
using MyBlog.Model.Domains;
using MyBlog.Model.Dto;

public class LikeService : ILikeService
{
    private readonly BlogDbContext _context;
    private readonly IBlogRepository blogRepository;

    public LikeService(IBlogRepository blogRepository)
    {
        this.blogRepository = blogRepository;
    }

    public async Task<LikeResponseDto> ToggleLikeAsync(int articleId, int userId)
    {
        try
        {
          
            var existingLike = await blogRepository.GetLikeByUserIdAndArticleId(userId, articleId);

            var article = await blogRepository.GetArticleById(articleId);

            if (article == null)
                throw new NotFound("Article not found");

            if (existingLike == null)
            {
                var like = new Like
                {
                    ArticleId = articleId,
                    UserId = userId,
                    HasLiked = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await blogRepository.CreateLikeAsync(like);
            }
            else
            {
                await blogRepository.UpdateLikeToggleAsync(existingLike);
                
                
            }

            int likeCounts = await blogRepository.GetLikeCountForArticle(articleId);
            return new LikeResponseDto
            {
                TotalLikes = likeCounts,
                IsLikedByUser = existingLike == null
            };
            
        }
        catch (Exception)
        {
            throw new NotFound("Article not found");
        }
        
    }

    public async Task<LikeResponseDto> GetLikeStatusAsync(int articleId, int userId)
    {
        var isLikedByUser = await blogRepository.GetLikeByUserIdAndArticleId(userId, articleId);

        int likeCounts = await blogRepository.GetLikeCountForArticle(articleId);

        return new LikeResponseDto
        {
            TotalLikes = likeCounts,
            IsLikedByUser = isLikedByUser == null
        };
    }
}