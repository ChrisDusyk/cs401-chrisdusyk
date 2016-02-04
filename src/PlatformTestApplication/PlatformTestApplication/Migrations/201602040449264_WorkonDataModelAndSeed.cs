namespace PlatformTestApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkonDataModelAndSeed : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "CS401_DB.Customers", newSchema: "dbo");
            CreateTable(
                "dbo.CustomerProducts",
                c => new
                    {
                        CustomerProductID = c.Int(nullable: false, identity: true),
                        SalesPersonID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        SoldDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.CustomerProductID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .ForeignKey("dbo.Employees", t => t.SalesPersonID)
                .Index(t => t.SalesPersonID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50, storeType: "nvarchar"),
                        LastName = c.String(maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(maxLength: 100, storeType: "nvarchar"),
                        AspNetUserID = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(maxLength: 5, storeType: "nvarchar"),
                        ProductName = c.String(maxLength: 200, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerProducts", "SalesPersonID", "dbo.Employees");
            DropForeignKey("dbo.CustomerProducts", "CustomerID", "dbo.Customers");
            DropIndex("dbo.CustomerProducts", new[] { "CustomerID" });
            DropIndex("dbo.CustomerProducts", new[] { "SalesPersonID" });
            DropTable("dbo.Products");
            DropTable("dbo.Employees");
            DropTable("dbo.CustomerProducts");
            MoveTable(name: "dbo.Customers", newSchema: "CS401_DB");
        }
    }
}
