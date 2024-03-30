﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace web_store_server.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            PasswordResets = new HashSet<PasswordReset>();
            Posts = new HashSet<Post>();
            UserOauthClientRequests = new HashSet<UserOauthClientRequest>();
            UserOauthIdentities = new HashSet<UserOauthIdentity>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<PasswordReset> PasswordResets { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserOauthClientRequest> UserOauthClientRequests { get; set; }
        public virtual ICollection<UserOauthIdentity> UserOauthIdentities { get; set; }
    }
}