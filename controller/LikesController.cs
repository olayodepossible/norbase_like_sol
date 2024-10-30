//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LikesController : ControllerBase
{

    private readonly ApplicationDbContext _context;

    private readonly ILikeService _likeService;

    public LikesController(ApplicationDbContext context, ILikeService likeService)
    {
        _context = context;
        _likeService = likeService;
    }

    [HttpPost("{articleId}/toggle")]
    public async Task<ActionResult<LikeResponseDto>> ToggleLike(string articleId)
    {
        // Extract user ID from JWT token
        // var userId = int.Parse(User.FindFirst("UserId")?.Value);
        using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        var result = await _likeService.ToggleLikeAsync(articleId, transaction);
        // var result = await _likeService.ToggleLikeAsync(articleId, userId, transaction);
        return Ok(result);
    }

    [HttpGet("{articleId}/status")]
    public async Task<ActionResult<LikeResponseDto>> GetLikeStatus(string articleId)
    {
        // var userId = User.GetUserId();
        var result = await _likeService.GetLikeStatusAsync(articleId);
        // var result = await _likeService.GetLikeStatusAsync(articleId, userId);
        return Ok(result);
    }
}



/*

//----------------------2 to scale

[HttpPost("toggle")]
public async Task<IActionResult> ToggleLike(int articleId)
{
    var userId = int.Parse(User.FindFirst("UserId")?.Value);

    // 1. Rate Limiting Check (Redis)
    if (await IsRateLimited(userId))
    {
        return StatusCode(StatusCodes.Status429TooManyRequests, "Rate limit exceeded.");
    }

    // 2. Toggle in Redis
    var cacheKey = $"Article:{articleId}:LikeCount";
    var cacheCount = await _cache.GetStringAsync(cacheKey);
    var likeCount = cacheCount != null ? int.Parse(cacheCount) : 0;

    var toggle = await _likeService.ToggleLikeAsync(articleId, userId);
    if (toggle)
    {
        likeCount++; // Increment if liked
    }
    else
    {
        likeCount--; // Decrement if unliked
    }
    await _cache.SetStringAsync(cacheKey, likeCount.ToString());

    // 3. Send to Queue for async DB update
    await _queueClient.SendMessageAsync(new LikeMessage
    {
        ArticleId = articleId,
        UserId = userId,
        Action = toggle ? "like" : "unlike"
    });

    return Ok(new { Liked = toggle, Count = likeCount });
}


*/
