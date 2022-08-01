namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePriceFromPlotTypesTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlotTypes", "SecondBurialPrice");
            DropColumn("dbo.PlotTypes", "ClearancePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlotTypes", "ClearancePrice", c => c.Single(nullable: false));
            AddColumn("dbo.PlotTypes", "SecondBurialPrice", c => c.Single(nullable: false));
        }
    }
}
