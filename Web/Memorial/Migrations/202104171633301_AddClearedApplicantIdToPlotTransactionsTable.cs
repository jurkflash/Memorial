namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClearedApplicantIdToPlotTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotTransactions", "ClearedApplicantId", c => c.Int());
            CreateIndex("dbo.PlotTransactions", "ClearedApplicantId");
            AddForeignKey("dbo.PlotTransactions", "ClearedApplicantId", "dbo.Applicants", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotTransactions", "ClearedApplicantId", "dbo.Applicants");
            DropIndex("dbo.PlotTransactions", new[] { "ClearedApplicantId" });
            DropColumn("dbo.PlotTransactions", "ClearedApplicantId");
        }
    }
}
