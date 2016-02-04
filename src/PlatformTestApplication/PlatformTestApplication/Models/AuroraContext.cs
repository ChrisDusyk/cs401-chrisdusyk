namespace PlatformTestApplication.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class AuroraContext : DbContext
	{
		public AuroraContext()
			: base("name=AuroraContext")
		{
		}

		public virtual DbSet<Customer> Customers { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customer>()
				.Property(e => e.CustomerName)
				.IsUnicode(false);

			modelBuilder.Entity<Customer>()
				.Property(e => e.Address)
				.IsUnicode(false);

			modelBuilder.Entity<Customer>()
				.Property(e => e.City)
				.IsUnicode(false);

			modelBuilder.Entity<Customer>()
				.Property(e => e.Province)
				.IsUnicode(false);

			modelBuilder.Entity<Customer>()
				.Property(e => e.PostalCode)
				.IsUnicode(false);
		}
	}
}
