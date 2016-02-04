namespace PlatformTestApplication.Migrations
{
	using System.Data.Entity.Migrations;
	using PlatformTestApplication.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<PlatformTestApplication.Models.AuroraContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;

			SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
		}

		protected override void Seed(PlatformTestApplication.Models.AuroraContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//

			//context.Products.AddOrUpdate(
			//	p => p.ProductID,
			//	new Product() { ProductCode = "PROD1", ProductName = "Product 1" },
			//	new Product() { ProductCode = "PROD2", ProductName = "Product 2" }
			//);

			//context.Employees.AddOrUpdate(
			//	e => e.EmployeeID,
			//	new Employee() { FirstName = "Roger", LastName = "Gonzalez", Email = "rgonzalez0@company.com" },
			//	new Employee() { FirstName = "James", LastName = "Rose", Email = "jrose1@company.com" },
			//	new Employee() { FirstName = "Victor", LastName = "Kennedy", Email = "vkennedy2@company.com" },
			//	new Employee() { FirstName = "Adam", LastName = "White", Email = "awhite3@company.com" },
			//	new Employee() { FirstName = "Daniel", LastName = "Lane", Email = "dlane4@company.com" }
			//);
		}
	}
}
