﻿namespace web_store_mvc.Dtos
{
    public class CreateProductDto
    {
        public int SubcategoryId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long? Price { get; set; }
        public int Stock { get; set; }
        public string Sku { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public List<string> Tags { get; set; } = null!;
        public bool Active { get; set; }
    }
}
