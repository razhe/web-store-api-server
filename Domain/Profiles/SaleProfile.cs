using AutoMapper;
using web_store_server.Domain.Dtos.Admin;
using web_store_server.Domain.Dtos.ProductSale;
using web_store_server.Domain.Dtos.Sales;
using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, GetSaleDto>()
                .ForMember(dest => 
                dest.OrderNumber,
                opt => opt.MapFrom(src => src.Order.OrderNumber));

            CreateMap<ProductSale, GetProductSaleDto>()
                .ForMember(dest =>
                dest.ProductName,
                opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<ProductSale, ReportDto>()
                .ForMember(dest =>
                    dest.RegisterDate,
                    opt => opt.MapFrom(src => src.Sale.CreatedAt.ToString("dd/MM/yyyy")))
                .ForMember(dest =>
                    dest.OrderNumber,
                    opt => opt.MapFrom(src => src.Sale.Order.OrderNumber))
                .ForMember(dest =>
                    dest.TotalSale,
                    opt => opt.MapFrom(src => src.Sale.Total))
                .ForMember(dest =>
                    dest.Product,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest =>
                    dest.Price,
                    opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest =>
                    dest.Total,
                    opt => opt.MapFrom(src => src.Subtotal))
                .ForMember(dest =>
                    dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
