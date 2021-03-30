namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFengShuiMasterIdToPlotTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotTransactions", "FengShuiMasterId", c => c.Int());
            CreateIndex("dbo.PlotTransactions", "FengShuiMasterId");
            AddForeignKey("dbo.PlotTransactions", "FengShuiMasterId", "dbo.FengShuiMasters", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotTransactions", "FengShuiMasterId", "dbo.FengShuiMasters");
            DropIndex("dbo.PlotTransactions", new[] { "FengShuiMasterId" });
            DropColumn("dbo.PlotTransactions", "FengShuiMasterId");
        }
    }
}
