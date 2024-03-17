namespace web_store_server.Domain.Dtos.Products
{
    public class GetProductDto
    {
        public Guid Id { get; set; } = new Guid();
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; } = null!;
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long Price { get; set; }
        public int Stock { get; set; }
        public string Sku { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public List<string> Tags { get; set; } = null!;
        public bool Active { get; set; }
    }
}
