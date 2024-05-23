namespace web_store_server.Domain.Dtos.Products
{
    public class CreateUpdateProductDto
    {
        public int SubcategoryId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long Price { get; set; }
        public int Stock { get; set; }
        public string Sku { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public List<string> Tags { get; set; } = null!;
        public bool Active { get; set; }

        public void MapToModel(Entities.Product p)
        {
            p.SubcategoryId = SubcategoryId;
            p.BrandId = BrandId;
            p.Name = Name;
            p.Description = Description;
            p.Price = Price;
            p.Stock = Stock;
            p.Sku = Sku;
            p.Slug = Slug;
            p.Tags = Tags;
            p.Active = Active;
        }
    }
}
