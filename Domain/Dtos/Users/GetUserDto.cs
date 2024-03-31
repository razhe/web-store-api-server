using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Dtos.Users
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public bool Active { get; set; }
    }
}
