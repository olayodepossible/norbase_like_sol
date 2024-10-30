[ApiController]
[Route("api/[controller]")]
public class LikesController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikesController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpPost("{articleId}/toggle")]
    public async Task<ActionResult<LikeResponseDto>> ToggleLike(string articleId)
    {
        var userId = User.GetUserId(); // Assuming you have a method to get the current user's ID
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