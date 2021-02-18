namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSiteIdColumnAndRenameCodeToNameForQuadrangleAreasTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuadrangleAreas", "SiteId", "dbo.Sites");
            DropIndex("dbo.QuadrangleAreas", new[] { "SiteId" });
            AddColumn("dbo.QuadrangleAreas", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.QuadrangleAreas", "Code");
            DropColumn("dbo.QuadrangleAreas", "SiteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuadrangleAreas", "SiteId", c => c.Byte(nullable: false));
            AddColumn("dbo.QuadrangleAreas", "Code", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.QuadrangleAreas", "Name");
            CreateIndex("dbo.QuadrangleAreas", "SiteId");
            AddForeignKey("dbo.QuadrangleAreas", "SiteId", "dbo.Sites", "Id");
        }
    }
}
