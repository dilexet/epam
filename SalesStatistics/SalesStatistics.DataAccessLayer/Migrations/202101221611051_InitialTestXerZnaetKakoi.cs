namespace SalesStatistics.DataAccessLayer.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialTestXerZnaetKakoi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Manager");
        }
    }
}
