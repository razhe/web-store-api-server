﻿using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Dtos.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public int Role { get; set; }
        public bool Active { get; set; }
    }
}
