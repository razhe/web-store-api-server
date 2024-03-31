using AutoMapper;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Domain.Dtos.Subcategories;
using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Profiles
{
    public class ProductProfile :
        Profile
    {
        public ProductProfile()
        {
            #region Products

            CreateMap<Product, ProductDto>();

            CreateMap<Product, GetProductDto>()
                .ForMember(dest =>
                    dest.SubcategoryName, opt => opt.MapFrom(src => src.Subcategory.Name))
                .ForMember(dest =>
                    dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

            CreateMap<CreateProductDto, Product>()
                .ForMember(dest =>
                    dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            #endregion

            #region Categories

            CreateMap<ProductCategoryDto, ProductCategory>();
            CreateMap<ProductCategory, GetProductCategoryDto>().ReverseMap();

            #endregion
            #region Subcategories

            CreateMap<ProductSubcategoryDto, ProductSubcategory>();
            CreateMap<ProductSubcategory, GetProductSubcategoryDto>();

            #endregion





        }
    }
}
