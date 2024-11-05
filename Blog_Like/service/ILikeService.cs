using MyBlog.Model.Dto;

public interface ILikeService
{
    Task<LikeResponseDto> ToggleLikeAsync(string articleId, string userId);
    Task<LikeResponseDto> GetLikeStatusAsync(string articleId, string userId);
}