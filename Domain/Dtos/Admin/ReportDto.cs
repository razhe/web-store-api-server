namespace web_store_server.Domain.Dtos.Admin
{
    public class ReportDto
    {
        public string OrderNumber { get; set; } = null!;
        public DateTimeOffset RegisterDate { get; set; }
        public string TotalSale { get; set; } = null!;
        public string Product { get; set; } = null!;
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Total { get; set; } = null!;
    }
}
