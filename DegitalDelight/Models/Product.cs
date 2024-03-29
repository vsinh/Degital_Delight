﻿namespace DegitalDelight.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Picture { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Stock { get; set; } = 0;
        public string? Brand { get; set; }
        public DateTime CreatedDate { get; set; }
		public ProductType? ProductType { get; set; }
        public Warranty? Warranty { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public virtual ICollection<Supply> Supplies { get; } = new List<Supply>();
		public virtual ICollection<ProductDetail> ProductDetails { get; } = new List<ProductDetail>();
	}
}