namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuadrangleAreasTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuadrangleAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        SiteId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleAreas", "SiteId", "dbo.Sites");
            DropIndex("dbo.QuadrangleAreas", new[] { "SiteId" });
            DropTable("dbo.QuadrangleAreas");
        }
    }
}
