namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotItemToPlotTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotTransactions", "PlotItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.PlotTransactions", "PlotItemId");
            AddForeignKey("dbo.PlotTransactions", "PlotItemId", "dbo.PlotItems", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotTransactions", "PlotItemId", "dbo.PlotItems");
            DropIndex("dbo.PlotTransactions", new[] { "PlotItemId" });
            DropColumn("dbo.PlotTransactions", "PlotItemId");
        }
    }
}
