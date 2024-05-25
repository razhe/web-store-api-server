using web_store_server.Domain.Dtos.ProductSale;

namespace web_store_server.Domain.Dtos.Sales
{
    public class GetSaleDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public long Total { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<GetProductSaleDto> ProductSales { get; set; } = null!;
    }
}
