namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApplicantDeceasedsManyToManyTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicantDeceaseds", "Deceased_Id", "dbo.Deceaseds");
            DropForeignKey("dbo.ApplicantDeceaseds", "Applicant_Id", "dbo.Applicants");
            DropIndex("dbo.ApplicantDeceaseds", new[] { "Deceased_Id" });
            DropIndex("dbo.ApplicantDeceaseds", new[] { "Applicant_Id" });
            DropTable("dbo.ApplicantDeceaseds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicantDeceaseds",
                c => new
                    {
                        Deceased_Id = c.Int(nullable: false),
                        Applicant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Deceased_Id, t.Applicant_Id });
            
            CreateIndex("dbo.ApplicantDeceaseds", "Applicant_Id");
            CreateIndex("dbo.ApplicantDeceaseds", "Deceased_Id");
            AddForeignKey("dbo.ApplicantDeceaseds", "Applicant_Id", "dbo.Applicants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicantDeceaseds", "Deceased_Id", "dbo.Deceaseds", "Id", cascadeDelete: true);
        }
    }
}
