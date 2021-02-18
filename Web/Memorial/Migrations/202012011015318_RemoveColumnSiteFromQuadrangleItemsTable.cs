namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnSiteFromQuadrangleItemsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuadrangleItems", "SiteId", "dbo.Sites");
            DropIndex("dbo.QuadrangleItems", new[] { "SiteId" });
            DropColumn("dbo.QuadrangleItems", "SiteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuadrangleItems", "SiteId", c => c.Byte(nullable: false));
            CreateIndex("dbo.QuadrangleItems", "SiteId");
            AddForeignKey("dbo.QuadrangleItems", "SiteId", "dbo.Sites", "Id");
        }
    }
}
