namespace web_store_server.Domain.Dtos.Admin
{
    public class DashboardDto
    {
        public int TotalSales { get; set; }
        public string TotalProfit { get; set; } = null!;
        public int TotalProducts { get; set; }
        public IEnumerable<WeekSalesDto> LastWeekSales { get; set; } = null!;
    }
}
