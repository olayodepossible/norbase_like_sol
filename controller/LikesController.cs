[Authorize]
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
        var userId = int.Parse(User.FindFirst("UserId")?.Value);
        var result = await _likeService.ToggleLikeAsync(articleId, userId);
        return Ok(result);
    }

    [HttpGet("{articleId}/status")]
    public async Task<ActionResult<LikeResponseDto>> GetLikeStatus(string articleId)
    {
        var userId = User.GetUserId();
        var result = await _likeService.GetLikeStatusAsync(articleId, userId);
        return Ok(result);
    }
}
