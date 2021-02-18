namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuadrangleCentresTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuadrangleCentres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        SiteId = c.Byte(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            AddColumn("dbo.QuadrangleAreas", "QuadrangleCentreId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuadrangleAreas", "QuadrangleCentreId");
            AddForeignKey("dbo.QuadrangleAreas", "QuadrangleCentreId", "dbo.QuadrangleCentres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleCentres", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.QuadrangleAreas", "QuadrangleCentreId", "dbo.QuadrangleCentres");
            DropIndex("dbo.QuadrangleAreas", new[] { "QuadrangleCentreId" });
            DropIndex("dbo.QuadrangleCentres", new[] { "SiteId" });
            DropColumn("dbo.QuadrangleAreas", "QuadrangleCentreId");
            DropTable("dbo.QuadrangleCentres");
        }
    }
}
