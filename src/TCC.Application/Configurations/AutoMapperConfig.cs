using AutoMapper;
using TCC.Application.Views;
using TCC.Business.Models;

namespace TCC.Application.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserView>().ReverseMap();
            CreateMap<Group, GroupView>().ReverseMap();
            CreateMap<Category, CategoryView>().ReverseMap();
        }
    }
}
