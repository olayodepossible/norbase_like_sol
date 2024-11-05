using AutoMapper;
using MyBlog.Model.Domains;
using MyBlog.Model.Dto;

namespace MyBlog.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Article, ArticleDto>().ReverseMap();

        }
    }
}
