namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicantAndDeceasedLinkToPlotsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deceaseds", "PlotId", c => c.Int());
            AddColumn("dbo.Plots", "ApplicantId", c => c.Int());
            CreateIndex("dbo.Deceaseds", "PlotId");
            CreateIndex("dbo.Plots", "ApplicantId");
            AddForeignKey("dbo.Plots", "ApplicantId", "dbo.Applicants", "Id");
            AddForeignKey("dbo.Deceaseds", "PlotId", "dbo.Plots", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deceaseds", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.Plots", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Plots", new[] { "ApplicantId" });
            DropIndex("dbo.Deceaseds", new[] { "PlotId" });
            DropColumn("dbo.Plots", "ApplicantId");
            DropColumn("dbo.Deceaseds", "PlotId");
        }
    }
}
