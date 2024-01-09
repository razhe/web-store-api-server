using AutoMapper;
using web_store_mvc.Dtos;
using web_store_server.Domain.Models.Entities;

namespace web_store_mvc.Profiles
{
    public class ProductProfile :
        Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDto>()
                .ForMember(dest =>
                    dest.SubcategoryName,
                    opt => opt.MapFrom(src => src.Subcategory.Name));

            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}
