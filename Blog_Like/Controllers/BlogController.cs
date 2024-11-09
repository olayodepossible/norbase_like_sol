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

        [HttpPost("toggle/{articleId:int}/{userId:int}")]
        public async Task<ActionResult<LikeResponseDto>> ToggleLike([FromRoute] int articleId, [FromRoute] int userId)
        {
            var result = await _likeService.ToggleLikeAsync(articleId, userId);
            return Ok(result);
        }

        [HttpGet("status/{articleId:int}/{userId:int}")]
        public async Task<ActionResult<LikeResponseDto>> GetLikeStatus([FromRoute] int articleId, [FromRoute] int userId)
        {
            var result = await _likeService.GetLikeStatusAsync(articleId, userId);
            return Ok(result);
        }
    }
}
