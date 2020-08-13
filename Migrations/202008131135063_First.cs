namespace FadlonRealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brokers",
                c => new
                    {
                        BrokerID = c.Int(nullable: false, identity: true),
                        BrokerName = c.String(nullable: false),
                        BrokerPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BrokerID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerFirstName = c.String(nullable: false),
                        CustomerLastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        Age = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Deals",
                c => new
                    {
                        DealID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        PropertyID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DealID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Properties", t => t.PropertyID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.PropertyID);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropertyID = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(nullable: false),
                        PropertyType = c.String(nullable: false),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "PropertyID", "dbo.Properties");
            DropForeignKey("dbo.Deals", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Deals", new[] { "PropertyID" });
            DropIndex("dbo.Deals", new[] { "CustomerID" });
            DropTable("dbo.Properties");
            DropTable("dbo.Deals");
            DropTable("dbo.Customers");
            DropTable("dbo.Brokers");
        }
    }
}
