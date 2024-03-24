namespace web_store_server.Domain.Dtos.Sales
{
    public class CreateSaleDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
