using MyBlog.Model.Dto;

public interface ILikeService
{
    Task<LikeResponseDto> ToggleLikeAsync(int articleId, int userId);
    Task<LikeResponseDto> GetLikeStatusAsync(int articleId, int userId);
}