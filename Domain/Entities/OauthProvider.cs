﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace web_store_server.Domain.Entities
{
    public partial class OauthProvider
    {
        public OauthProvider()
        {
            UserOauthRequests = new HashSet<UserOauthRequest>();
        }

        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<UserOauthRequest> UserOauthRequests { get; set; }
    }
}