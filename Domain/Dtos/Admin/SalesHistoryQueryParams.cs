namespace web_store_server.Domain.Dtos.Admin
{
    public class SalesHistoryQueryParams
    {
        public string SearchTerm { get; set; } = null!;
        public string? OrderNumber { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
    }
}
