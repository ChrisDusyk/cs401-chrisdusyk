namespace PlatformTestApplication.Models
{
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration;
	using System.Linq;

	public partial class AuroraContext : DbContext
	{
		public AuroraContext() : base("name=AuroraContext")
		{
		}

		public virtual DbSet<Customer> Customers { get; set; }

		public virtual DbSet<Product> Products { get; set; }

		public virtual DbSet<CustomerProduct> CustomerProducts { get; set; }

		public virtual DbSet<Employee> Employees { get; set; }

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

			//modelBuilder.Entity<CustomerProduct>()
			//	.HasRequired(cp => cp.Customer)
			//	.WithMany(c => c.CustomerProducts)
			//	.HasForeignKey(cp => cp.CustomerID)
			//	.WillCascadeOnDelete(false);

			modelBuilder.Entity<CustomerProduct>()
				.HasRequired(cp => cp.SalesPerson)
				.WithMany(e => e.SoldProducts)
				.HasForeignKey(cp => cp.SalesPersonID)
				.WillCascadeOnDelete(false);
		}
	}
}
