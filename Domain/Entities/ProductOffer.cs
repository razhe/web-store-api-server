﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace web_store_server.Domain.Entities
{
    public partial class ProductOffer
    {
        public int Id { get; set; }
        public Guid OfferId { get; set; }
        public Guid ProductId { get; set; }
        public long OfferPrice { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual Product Product { get; set; }
    }
}