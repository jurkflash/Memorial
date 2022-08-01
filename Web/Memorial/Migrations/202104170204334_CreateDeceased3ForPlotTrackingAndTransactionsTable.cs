namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDeceased3ForPlotTrackingAndTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotTransactions", "Deceased3Id", c => c.Int());
            AddColumn("dbo.PlotTrackings", "Deceased3Id", c => c.Int());
            CreateIndex("dbo.PlotTransactions", "Deceased3Id");
            CreateIndex("dbo.PlotTrackings", "Deceased3Id");
            AddForeignKey("dbo.PlotTransactions", "Deceased3Id", "dbo.Deceaseds", "Id");
            AddForeignKey("dbo.PlotTrackings", "Deceased3Id", "dbo.Deceaseds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotTrackings", "Deceased3Id", "dbo.Deceaseds");
            DropForeignKey("dbo.PlotTransactions", "Deceased3Id", "dbo.Deceaseds");
            DropIndex("dbo.PlotTrackings", new[] { "Deceased3Id" });
            DropIndex("dbo.PlotTransactions", new[] { "Deceased3Id" });
            DropColumn("dbo.PlotTrackings", "Deceased3Id");
            DropColumn("dbo.PlotTransactions", "Deceased3Id");
        }
    }
}
