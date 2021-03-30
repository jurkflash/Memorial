namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePlotTypeFromPlotItemsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlotItems", "PlotTypeId", "dbo.PlotTypes");
            DropIndex("dbo.PlotItems", new[] { "PlotTypeId" });
            DropColumn("dbo.PlotItems", "PlotTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlotItems", "PlotTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.PlotItems", "PlotTypeId");
            AddForeignKey("dbo.PlotItems", "PlotTypeId", "dbo.PlotTypes", "Id");
        }
    }
}
