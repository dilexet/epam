namespace SalesStatistics.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sale", "PurchaseDate", c => c.DateTime());
            AddColumn("dbo.Product", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Sale", "Cost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sale", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Product", "Cost");
            DropColumn("dbo.Sale", "PurchaseDate");
        }
    }
}
