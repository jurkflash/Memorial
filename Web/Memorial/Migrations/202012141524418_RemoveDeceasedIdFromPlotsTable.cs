namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDeceasedIdFromPlotsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deceaseds", "PlotId", "dbo.Plots");
            DropIndex("dbo.Deceaseds", new[] { "PlotId" });
            AddColumn("dbo.PlotTransactions", "DeceasedId", c => c.Int());
            CreateIndex("dbo.PlotTransactions", "DeceasedId");
            AddForeignKey("dbo.PlotTransactions", "DeceasedId", "dbo.Deceaseds", "Id");
            DropColumn("dbo.Deceaseds", "PlotId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deceaseds", "PlotId", c => c.Int());
            DropForeignKey("dbo.PlotTransactions", "DeceasedId", "dbo.Deceaseds");
            DropIndex("dbo.PlotTransactions", new[] { "DeceasedId" });
            DropColumn("dbo.PlotTransactions", "DeceasedId");
            CreateIndex("dbo.Deceaseds", "PlotId");
            AddForeignKey("dbo.Deceaseds", "PlotId", "dbo.Plots", "Id");
        }
    }
}
