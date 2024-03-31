using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Dtos.Categories
{
    public class ProductCategoryDto
    {
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        public void MapToModel(ProductCategory pc)
        {
            pc.Name = Name;
            pc.Active = Active;
        }
    }
}
