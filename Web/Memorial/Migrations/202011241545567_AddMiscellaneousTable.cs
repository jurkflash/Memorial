namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Miscellaneous",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Remark = c.String(maxLength: 255),
                        SiteId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Miscellaneous", "SiteId", "dbo.Sites");
            DropIndex("dbo.Miscellaneous", new[] { "SiteId" });
            DropTable("dbo.Miscellaneous");
        }
    }
}
