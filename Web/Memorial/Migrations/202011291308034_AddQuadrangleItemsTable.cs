namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuadrangleItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuadrangleItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        SiteId = c.Byte(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleItems", "SiteId", "dbo.Sites");
            DropIndex("dbo.QuadrangleItems", new[] { "SiteId" });
            DropTable("dbo.QuadrangleItems");
        }
    }
}
