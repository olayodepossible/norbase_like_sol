using MyBlog.Model.Dto;

public interface ILikeService
{
    Task<LikeResponseDto> ToggleLikeAsync(Guid articleId, Guid userId);
    Task<LikeResponseDto> GetLikeStatusAsync(Guid articleId, Guid userId);
}