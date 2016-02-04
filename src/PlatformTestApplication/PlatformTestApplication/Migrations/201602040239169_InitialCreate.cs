namespace PlatformTestApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CS401_DB.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(maxLength: 200, unicode: false),
                        Address = c.String(maxLength: 200, unicode: false),
                        City = c.String(maxLength: 100, unicode: false),
                        Province = c.String(maxLength: 30, unicode: false),
                        PostalCode = c.String(maxLength: 7, unicode: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropTable("CS401_DB.Customers");
        }
    }
}
