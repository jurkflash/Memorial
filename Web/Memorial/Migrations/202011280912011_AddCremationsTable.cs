namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCremationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cremations",
                c => new
                    {
                        Id = c.Byte(nullable: false),
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
            DropForeignKey("dbo.Cremations", "SiteId", "dbo.Sites");
            DropIndex("dbo.Cremations", new[] { "SiteId" });
            DropTable("dbo.Cremations");
        }
    }
}
