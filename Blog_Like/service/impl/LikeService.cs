using MyBlog.Db;
using MyBlog.Model.Dto;

public class LikeService : ILikeService
{
    private readonly BlogDbContext _context;

    public LikeService(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<LikeResponseDto> ToggleLikeAsync(string articleId, string transaction)
    // public async Task<LikeResponseDto> ToggleLikeAsync(string articleId, string userId, string transaction)
    {
        try
        {
            var existingLike = await _context.Likes.FirstOrDefaultAsync(l => l.ArticleId == articleId);

            // var existingLike = await _context.Likes
            // .FirstOrDefaultAsync(l => l.ArticleId == articleId && l.UserId == userId);

            var article = await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                throw new NotFoundException("Article not found");

            if (existingLike == null)
            {
                // Add new like
                var like = new Like
                {
                    ArticleId = articleId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };
                await _context.Likes.AddAsync(like);
                article.LikesCount++;
            }
            else
            {
            // Unlike the article
                _context.Likes.Remove(existingLike);
                article.LikesCount--;
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new LikeResponseDto
            {
                TotalLikes = article.LikesCount,
                IsLikedByUser = existingLike == null
            };
            
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
        }
        
    }




    public async Task<LikeResponseDto> GetLikeStatusAsync(string articleId)
    // public async Task<LikeResponseDto> GetLikeStatusAsync(string articleId, string userId)
    {
        var article = await _context.Articles
            .FirstOrDefaultAsync(a => a.Id == articleId);

        if (article == null)
            throw new NotFoundException("Article not found");

        var isLikedByUser = await _context.Likes
            .AnyAsync(l => l.ArticleId == articleId && l.UserId == userId);

        return new LikeResponseDto
        {
            TotalLikes = article.LikesCount,
            IsLikedByUser = isLikedByUser
        };
    }
}