namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApplicantIdFromPlotsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Plots", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Plots", new[] { "ApplicantId" });
            DropColumn("dbo.Plots", "ApplicantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plots", "ApplicantId", c => c.Int());
            CreateIndex("dbo.Plots", "ApplicantId");
            AddForeignKey("dbo.Plots", "ApplicantId", "dbo.Applicants", "Id");
        }
    }
}
