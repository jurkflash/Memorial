namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlotTrackingsToCemeteryTrackingsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlotTrackings", newName: "CemeteryTrackings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CemeteryTrackings", newName: "PlotTrackings");
        }
    }
}
