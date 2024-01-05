using DegitalDelight.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<CartItem> CartItems { get; set; }
		public virtual DbSet<Comment> Comments { get; set; }
		public virtual DbSet<Discount> Discounts { get; set; }
		public virtual DbSet<Order> Order { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductType> ProductTypes { get; set; }
		public virtual DbSet<Report> Reports { get; set; }
		public virtual DbSet<ReportDetail> ReportDetail { get; set; }
		public virtual DbSet<Supply> Supplies { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Warranty> Warrantys { get; set; }
		public virtual DbSet<Favorite> Favorites { get; set; }
		public virtual DbSet<ProductDetail> ProductDetails { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//Seeding a  'Administrator' role to AspNetRoles table
			modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });


			//a hasher to hash the password before seeding the user to the db
			var hasher = new PasswordHasher<User>();


			//Seeding the User to AspNetUsers table
			modelBuilder.Entity<User>().HasData(
				new User
				{
					Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
					UserName = "admin@admin",
					NormalizedUserName = "ADMIN@ADMIN",
					PasswordHash = hasher.HashPassword(null, "123456")
				}
			);


			//Seeding the relation between our user and role to AspNetUserRoles table
			modelBuilder.Entity<IdentityUserRole<string>>().HasData(
				new IdentityUserRole<string>
				{
					RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
					UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
				}
			);


		}
	}
}
