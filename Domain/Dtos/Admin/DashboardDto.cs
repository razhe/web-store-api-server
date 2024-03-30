namespace web_store_server.Domain.Dtos.Admin
{
    public class DashboardDto
    {
        public int TotalSales { get; set; }
        public long TotalEarning { get; set; }
        public IEnumerable<WeekSalesDto> LastWeekSales { get; set; } = null!;
    }
}
