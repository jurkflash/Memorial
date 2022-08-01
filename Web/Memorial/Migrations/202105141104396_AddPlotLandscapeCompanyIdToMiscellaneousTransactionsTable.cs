namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotLandscapeCompanyIdToMiscellaneousTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MiscellaneousTransactions", "PlotLandscapeCompanyId", c => c.Int());
            CreateIndex("dbo.MiscellaneousTransactions", "PlotLandscapeCompanyId");
            AddForeignKey("dbo.MiscellaneousTransactions", "PlotLandscapeCompanyId", "dbo.PlotLandscapeCompanies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MiscellaneousTransactions", "PlotLandscapeCompanyId", "dbo.PlotLandscapeCompanies");
            DropIndex("dbo.MiscellaneousTransactions", new[] { "PlotLandscapeCompanyId" });
            DropColumn("dbo.MiscellaneousTransactions", "PlotLandscapeCompanyId");
        }
    }
}
