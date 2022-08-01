namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAppliantsAndDeceasedsRelationshipManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deceaseds", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Deceaseds", new[] { "ApplicantId" });
            CreateTable(
                "dbo.ApplicantDeceaseds",
                c => new
                    {
                        Deceased_Id = c.Int(nullable: false),
                        Applicant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Deceased_Id, t.Applicant_Id })
                .ForeignKey("dbo.Deceaseds", t => t.Deceased_Id, cascadeDelete: true)
                .ForeignKey("dbo.Applicants", t => t.Applicant_Id, cascadeDelete: true)
                .Index(t => t.Deceased_Id)
                .Index(t => t.Applicant_Id);
            
            DropColumn("dbo.Deceaseds", "ApplicantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deceaseds", "ApplicantId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ApplicantDeceaseds", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.ApplicantDeceaseds", "Deceased_Id", "dbo.Deceaseds");
            DropIndex("dbo.ApplicantDeceaseds", new[] { "Applicant_Id" });
            DropIndex("dbo.ApplicantDeceaseds", new[] { "Deceased_Id" });
            DropTable("dbo.ApplicantDeceaseds");
            CreateIndex("dbo.Deceaseds", "ApplicantId");
            AddForeignKey("dbo.Deceaseds", "ApplicantId", "dbo.Applicants", "Id");
        }
    }
}
