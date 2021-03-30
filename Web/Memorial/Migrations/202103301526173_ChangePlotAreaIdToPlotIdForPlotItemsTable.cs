namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlotAreaIdToPlotIdForPlotItemsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlotItems", "PlotAreaId", "dbo.PlotAreas");
            DropIndex("dbo.PlotItems", new[] { "PlotAreaId" });
            AddColumn("dbo.PlotItems", "PlotId", c => c.Int(nullable: false));
            CreateIndex("dbo.PlotItems", "PlotId");
            AddForeignKey("dbo.PlotItems", "PlotId", "dbo.Plots", "Id");
            DropColumn("dbo.PlotItems", "PlotAreaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlotItems", "PlotAreaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlotItems", "PlotId", "dbo.Plots");
            DropIndex("dbo.PlotItems", new[] { "PlotId" });
            DropColumn("dbo.PlotItems", "PlotId");
            CreateIndex("dbo.PlotItems", "PlotAreaId");
            AddForeignKey("dbo.PlotItems", "PlotAreaId", "dbo.PlotAreas", "Id");
        }
    }
}
