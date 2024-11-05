using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Db;
using MyBlog.Model.Dto;

namespace Blog_Like.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogDbContext _context;

        private readonly ILikeService _likeService;

        public LikesController(BlogDbContext context, ILikeService likeService)
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
}
