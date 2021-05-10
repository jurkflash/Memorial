namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTransferredFromApplicantIdToQuadrangleTrackingsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuadrangleTrackings", "TransferredFromApplicantId", "dbo.Applicants");
            DropIndex("dbo.QuadrangleTrackings", new[] { "TransferredFromApplicantId" });
            DropColumn("dbo.QuadrangleTrackings", "TransferredFromApplicantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuadrangleTrackings", "TransferredFromApplicantId", c => c.Int());
            CreateIndex("dbo.QuadrangleTrackings", "TransferredFromApplicantId");
            AddForeignKey("dbo.QuadrangleTrackings", "TransferredFromApplicantId", "dbo.Applicants", "Id");
        }
    }
}
