namespace web_store_server.Domain.Dtos.Categories
{
    public class GetProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
