public class LikeService : ILikeService
{
    private readonly ApplicationDbContext _context;

    public LikeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LikeResponseDto> ToggleLikeAsync(string articleId, string userId)
    {
        var existingLike = await _context.Likes
            .FirstOrDefaultAsync(l => l.ArticleId == articleId && l.UserId == userId);

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
            _context.Likes.Add(like);
            article.LikesCount++;
        }
        else
        {
            // Remove existing like
            _context.Likes.Remove(existingLike);
            article.LikesCount--;
        }

        await _context.SaveChangesAsync();

        return new LikeResponseDto
        {
            TotalLikes = article.LikesCount,
            IsLikedByUser = existingLike == null
        };
    }

    public async Task<LikeResponseDto> GetLikeStatusAsync(string articleId, string userId)
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