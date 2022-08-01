namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsOrderColumnToPlotItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotItems", "isOrder", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlotItems", "isOrder");
        }
    }
}
