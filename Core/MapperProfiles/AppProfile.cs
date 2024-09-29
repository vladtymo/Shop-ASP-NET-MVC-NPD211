using AutoMapper;
using Data.Entities;
using Core.Models;

namespace Core.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CreateProductModel, Product>();
            CreateMap<Product, ProductModel>();
        }
    }
}
