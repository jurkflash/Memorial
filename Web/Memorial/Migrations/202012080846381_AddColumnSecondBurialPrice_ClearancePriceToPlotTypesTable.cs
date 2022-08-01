namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnSecondBurialPrice_ClearancePriceToPlotTypesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotTypes", "SecondBurialPrice", c => c.Single(nullable: false));
            AddColumn("dbo.PlotTypes", "ClearancePrice", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlotTypes", "ClearancePrice");
            DropColumn("dbo.PlotTypes", "SecondBurialPrice");
        }
    }
}
