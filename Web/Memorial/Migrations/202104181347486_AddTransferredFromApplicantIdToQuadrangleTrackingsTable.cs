namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransferredFromApplicantIdToQuadrangleTrackingsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTrackings", "TransferredFromApplicantId", c => c.Int());
            CreateIndex("dbo.QuadrangleTrackings", "TransferredFromApplicantId");
            AddForeignKey("dbo.QuadrangleTrackings", "TransferredFromApplicantId", "dbo.Applicants", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTrackings", "TransferredFromApplicantId", "dbo.Applicants");
            DropIndex("dbo.QuadrangleTrackings", new[] { "TransferredFromApplicantId" });
            DropColumn("dbo.QuadrangleTrackings", "TransferredFromApplicantId");
        }
    }
}
