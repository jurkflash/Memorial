namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAncestorAreasTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AncestorAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        SiteId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            AddColumn("dbo.Ancestors", "AncestorAreaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ancestors", "AncestorAreaId");
            AddForeignKey("dbo.Ancestors", "AncestorAreaId", "dbo.AncestorAreas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AncestorAreas", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Ancestors", "AncestorAreaId", "dbo.AncestorAreas");
            DropIndex("dbo.Ancestors", new[] { "AncestorAreaId" });
            DropIndex("dbo.AncestorAreas", new[] { "SiteId" });
            DropColumn("dbo.Ancestors", "AncestorAreaId");
            DropTable("dbo.AncestorAreas");
        }
    }
}
