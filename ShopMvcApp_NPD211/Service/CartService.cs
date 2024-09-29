using AutoMapper;
using Data;
using Data.Entities;
using ShopMvcApp_NPD211.Extensions;
using Core.Models;
using Core.Services;

namespace ShopMvcApp_NPD211.Services
{
    public class CartService : ICartService
    {
        const string cartKey = "cartItems";
        private readonly ShopMvcDbContext ctx;
        private readonly IMapper mapper;
        HttpContext httpContext;

        public CartService(IHttpContextAccessor contextAccessor, 
                            ShopMvcDbContext ctx,
                            IMapper mapper)
        {
            this.httpContext = contextAccessor.HttpContext;
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public void Add(int id)
        {
            var ids = httpContext.Session.Get<List<int>>(cartKey) ?? [];
            ids.Add(id);

            httpContext.Session.Set(cartKey, ids);
        }

        public IEnumerable<int> GetIds()
        {
            return httpContext.Session.Get<List<int>>(cartKey) ?? [];
        }

        public IEnumerable<Product> GetProducts()
        {
            var ids = GetIds();
           return ctx.Products.Where(x => ids.Contains(x.Id)).ToList();
        }

        public IEnumerable<ProductModel> GetProductDtos()
        {
            return mapper.Map<IEnumerable<ProductModel>>(GetProducts());
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
