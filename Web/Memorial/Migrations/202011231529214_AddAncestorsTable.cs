namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAncestorsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ancestors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationX = c.Byte(nullable: false),
                        LocationY = c.Byte(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(nullable: false),
                        Label = c.String(),
                        Remark = c.String(maxLength: 255),
                        SiteId = c.Byte(nullable: false),
                        ApplicantId = c.Int(),
                        RelationshipTypeId = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.RelationshipTypes", t => t.RelationshipTypeId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId)
                .Index(t => t.ApplicantId)
                .Index(t => t.RelationshipTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ancestors", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Ancestors", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropForeignKey("dbo.Ancestors", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Ancestors", new[] { "RelationshipTypeId" });
            DropIndex("dbo.Ancestors", new[] { "ApplicantId" });
            DropIndex("dbo.Ancestors", new[] { "SiteId" });
            DropTable("dbo.Ancestors");
        }
    }
}
