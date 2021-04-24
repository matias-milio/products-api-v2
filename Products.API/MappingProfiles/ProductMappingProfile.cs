using Products.Application.Products.ResponseModels;
using Products.Domain;

namespace Products.API.MappingProfiles
{
    public class ProductMappingProfile : AutoMapper.Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponseModel>();
        }
    }
}
