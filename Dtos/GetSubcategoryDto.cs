﻿namespace web_store_mvc.Dtos
{
    public class GetSubcategoryDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
