namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicantDeceasedsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deceaseds", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropIndex("dbo.Deceaseds", new[] { "RelationshipTypeId" });
            CreateTable(
                "dbo.ApplicantDeceaseds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        DeceasedId = c.Int(nullable: false),
                        RelationshipTypeId = c.Byte(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .ForeignKey("dbo.RelationshipTypes", t => t.RelationshipTypeId)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId)
                .Index(t => t.RelationshipTypeId);
            
            DropColumn("dbo.Deceaseds", "RelationshipTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deceaseds", "RelationshipTypeId", c => c.Byte(nullable: false));
            DropForeignKey("dbo.ApplicantDeceaseds", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropForeignKey("dbo.ApplicantDeceaseds", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.ApplicantDeceaseds", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.ApplicantDeceaseds", new[] { "RelationshipTypeId" });
            DropIndex("dbo.ApplicantDeceaseds", new[] { "DeceasedId" });
            DropIndex("dbo.ApplicantDeceaseds", new[] { "ApplicantId" });
            DropTable("dbo.ApplicantDeceaseds");
            CreateIndex("dbo.Deceaseds", "RelationshipTypeId");
            AddForeignKey("dbo.Deceaseds", "RelationshipTypeId", "dbo.RelationshipTypes", "Id");
        }
    }
}
