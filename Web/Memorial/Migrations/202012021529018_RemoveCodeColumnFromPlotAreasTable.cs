namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCodeColumnFromPlotAreasTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlotAreas", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlotAreas", "Code", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
