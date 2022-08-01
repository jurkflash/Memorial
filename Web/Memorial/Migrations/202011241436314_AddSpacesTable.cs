namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpacesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Spaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Remark = c.String(maxLength: 255),
                        PricePerDay = c.Single(nullable: false),
                        AllowDoubleBook = c.Boolean(nullable: false),
                        ToleranceHour = c.Byte(nullable: false),
                        SiteId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Spaces", "SiteId", "dbo.Sites");
            DropIndex("dbo.Spaces", new[] { "SiteId" });
            DropTable("dbo.Spaces");
        }
    }
}
