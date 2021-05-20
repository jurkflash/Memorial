namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlotItemsToCemeteryItemsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlotItems", newName: "CemeteryItems");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CemeteryItems", newName: "PlotItems");
        }
    }
}
