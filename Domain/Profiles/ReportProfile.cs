using AutoMapper;
using web_store_server.Domain.Dtos.Admin;
using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Profiles
{
    public class ReportProfile : 
        Profile
    {
        public ReportProfile()
        {
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
