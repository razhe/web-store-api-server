namespace web_store_server.Domain.Dtos.Admin
{
    public class ReportQueryParams
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
