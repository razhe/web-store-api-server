namespace web_store_server.Dtos.Subcategories
{
    public class GetSubcategoryDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
