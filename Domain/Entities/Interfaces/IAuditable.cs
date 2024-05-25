namespace web_store_server.Domain.Entities.Interfaces
{
    public interface IAuditable
    {
        bool IsDeleted { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
    }
}
