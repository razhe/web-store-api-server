namespace web_store_server.Domain.Dtos.Users
{
    public class UserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public bool Active { get; set; }
    }
}
