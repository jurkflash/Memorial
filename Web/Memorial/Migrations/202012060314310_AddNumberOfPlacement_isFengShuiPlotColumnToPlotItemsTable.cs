namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberOfPlacement_isFengShuiPlotColumnToPlotItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotItems", "NumberOfPlacement", c => c.Byte(nullable: false));
            AddColumn("dbo.PlotItems", "isFengShuiPlot", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlotItems", "isFengShuiPlot");
            DropColumn("dbo.PlotItems", "NumberOfPlacement");
        }
    }
}
