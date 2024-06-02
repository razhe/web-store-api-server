namespace web_store_server.Domain.Dtos.Brands
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
