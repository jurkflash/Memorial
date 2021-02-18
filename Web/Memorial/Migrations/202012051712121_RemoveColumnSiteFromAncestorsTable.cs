namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnSiteFromAncestorsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ancestors", "SiteId", "dbo.Sites");
            DropIndex("dbo.Ancestors", new[] { "SiteId" });
            DropColumn("dbo.Ancestors", "SiteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ancestors", "SiteId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Ancestors", "SiteId");
            AddForeignKey("dbo.Ancestors", "SiteId", "dbo.Sites", "Id");
        }
    }
}
