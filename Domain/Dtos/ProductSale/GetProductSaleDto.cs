namespace web_store_server.Domain.Dtos.ProductSale
{
    public class GetProductSaleDto
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public long Subtotal { get; set; }
        public long UnitPrice { get; set; }
    }
}
