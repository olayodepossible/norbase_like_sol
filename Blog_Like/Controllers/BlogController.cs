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

        private readonly ILikeService _likeService;

        public BlogController( ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("toggle/{articleId:Guid}/{userId:Guid}")]
        //[Route("{userId:Guid}")]
        public async Task<ActionResult<LikeResponseDto>> ToggleLike([FromRoute] Guid articleId, [FromRoute] Guid userId)
        {
            // Extract user ID from JWT token
             // var userId = Guid.Parse(User.FindFirst("UserId")?.Value);
            var result = await _likeService.ToggleLikeAsync(articleId, userId);
            return Ok(result);
        }

        [HttpGet("status/{articleId:Guid}/{userId:Guid}")]
        //[Route("{userId:Guid}")]
        public async Task<ActionResult<LikeResponseDto>> GetLikeStatus([FromRoute] Guid articleId, [FromRoute] Guid userId)
        {
            //var userId = User.GetUserId();
            //var userId = Guid.Parse(User.FindFirst("UserId")?.Value);
            var result = await _likeService.GetLikeStatusAsync(articleId, userId);
            // var result = await _likeService.GetLikeStatusAsync(articleId, userId);
            return Ok(result);
        }
    }
}
