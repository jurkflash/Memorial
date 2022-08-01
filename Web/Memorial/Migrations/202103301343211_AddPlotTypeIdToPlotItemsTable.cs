namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotTypeIdToPlotItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotItems", "PlotTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.PlotItems", "PlotTypeId");
            AddForeignKey("dbo.PlotItems", "PlotTypeId", "dbo.PlotTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotItems", "PlotTypeId", "dbo.PlotTypes");
            DropIndex("dbo.PlotItems", new[] { "PlotTypeId" });
            DropColumn("dbo.PlotItems", "PlotTypeId");
        }
    }
}
