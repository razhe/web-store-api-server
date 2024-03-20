using AutoMapper;
using web_store_server.Domain.Dtos.Product;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Profiles
{
    public class ProductProfile :
        Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<Product, GetProductDto>()
                .ForMember(dest =>
                    dest.SubcategoryName, opt => opt.MapFrom(src => src.Subcategory.Name))
                .ForMember(dest =>
                    dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

            CreateMap<CreateProductDto, Product>()
                .ForMember(dest =>
                    dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest =>
                    dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}
