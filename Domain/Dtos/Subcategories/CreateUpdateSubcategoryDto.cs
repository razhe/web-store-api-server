using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Dtos.Subcategories
{
    public class CreateUpdateSubcategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        public void MapToModel(ProductSubcategory subcategory)
        {
            subcategory.Name = Name;
            subcategory.Active = Active;
        }
    }
}
