namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlotItemsToCemeteryItemsForCemeteryTransactionsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CemeteryTransactions", name: "PlotItemId", newName: "CemeteryItemId");
            RenameIndex(table: "dbo.CemeteryTransactions", name: "IX_PlotItemId", newName: "IX_CemeteryItemId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CemeteryTransactions", name: "IX_CemeteryItemId", newName: "IX_PlotItemId");
            RenameColumn(table: "dbo.CemeteryTransactions", name: "CemeteryItemId", newName: "PlotItemId");
        }
    }
}
