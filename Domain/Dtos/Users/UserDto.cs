﻿using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Dtos.Users
{
    public class UserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public bool Active { get; set; }

        public void MapToModel(User u)
        {
            u.Email = Email;
            u.Password = Password;
            u.Role = Role;
            u.Active = Active;
        }
    }
}
