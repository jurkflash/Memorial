namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCodeToPlotAreasTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotAreas", "Code", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlotAreas", "Code");
        }
    }
}
