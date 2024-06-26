﻿using web_store_server.Common.Enums;

namespace web_store_server.Domain.Dtos.Accounts
{
    public class GetAuthorizationDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public UserEnums.GrantTypes GrantType { get; set; }
    }
}
