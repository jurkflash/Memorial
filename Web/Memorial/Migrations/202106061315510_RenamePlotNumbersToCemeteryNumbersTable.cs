namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlotNumbersToCemeteryNumbersTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlotNumbers", newName: "CemeteryNumbers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CemeteryNumbers", newName: "PlotNumbers");
        }
    }
}
