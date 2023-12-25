﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace web_store_server.Domain.Entities
{
    public partial class AuthClient
    {
        public AuthClient()
        {
            AuthRequests = new HashSet<AuthRequest>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SecretKey { get; set; }
        public string Url { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<AuthRequest> AuthRequests { get; set; }
    }
}