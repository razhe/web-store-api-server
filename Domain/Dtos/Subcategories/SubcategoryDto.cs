﻿namespace web_store_server.Domain.Dtos.Subcategories
{
    public class SubcategoryDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
