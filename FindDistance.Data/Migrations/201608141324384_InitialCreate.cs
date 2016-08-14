namespace FindDistance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Route",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SingleRoute = c.String(),
                        Distance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Route");
        }
    }
}
