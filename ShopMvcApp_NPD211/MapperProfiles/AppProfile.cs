using AutoMapper;
using Data.Entities;
using ShopMvcApp_NPD211.Models;

namespace ShopMvcApp_NPD211.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CreateProductModel, Product>();
        }
    }
}
