﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace web_store_server.Domain.Entities
{
    public partial class Post
    {
        public Post()
        {
            PostGalleries = new HashSet<PostGallery>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual PostType Type { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostGallery> PostGalleries { get; set; }
    }
}