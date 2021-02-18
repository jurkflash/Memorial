namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnAddNumberOfPlacement_isFengShuiPlotFromPlotItemsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlotItems", "NumberOfPlacement");
            DropColumn("dbo.PlotItems", "isFengShuiPlot");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlotItems", "isFengShuiPlot", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlotItems", "NumberOfPlacement", c => c.Byte(nullable: false));
        }
    }
}
