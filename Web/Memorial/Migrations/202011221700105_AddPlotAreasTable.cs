namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotAreasTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlotAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        SiteId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotAreas", "SiteId", "dbo.Sites");
            DropIndex("dbo.PlotAreas", new[] { "SiteId" });
            DropTable("dbo.PlotAreas");
        }
    }
}
