namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlotTransactionsToCemeteryTransactionsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlotTransactions", newName: "CemeteryTransactions");
            RenameColumn(table: "dbo.Invoices", name: "PlotTransactionAF", newName: "CemeteryTransactionAF");
            RenameColumn(table: "dbo.PlotTrackings", name: "PlotTransactionAF", newName: "CemeteryTransactionAF");
            RenameColumn(table: "dbo.Receipts", name: "PlotTransactionAF", newName: "CemeteryTransactionAF");
            RenameColumn(table: "dbo.CemeteryTransactions", name: "TransferredPlotTransactionAF", newName: "TransferredCemeteryTransactionAF");
            RenameIndex(table: "dbo.CemeteryTransactions", name: "IX_TransferredPlotTransactionAF", newName: "IX_TransferredCemeteryTransactionAF");
            RenameIndex(table: "dbo.Invoices", name: "IX_PlotTransactionAF", newName: "IX_CemeteryTransactionAF");
            RenameIndex(table: "dbo.Receipts", name: "IX_PlotTransactionAF", newName: "IX_CemeteryTransactionAF");
            RenameIndex(table: "dbo.PlotTrackings", name: "IX_PlotTransactionAF", newName: "IX_CemeteryTransactionAF");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PlotTrackings", name: "IX_CemeteryTransactionAF", newName: "IX_PlotTransactionAF");
            RenameIndex(table: "dbo.Receipts", name: "IX_CemeteryTransactionAF", newName: "IX_PlotTransactionAF");
            RenameIndex(table: "dbo.Invoices", name: "IX_CemeteryTransactionAF", newName: "IX_PlotTransactionAF");
            RenameIndex(table: "dbo.CemeteryTransactions", name: "IX_TransferredCemeteryTransactionAF", newName: "IX_TransferredPlotTransactionAF");
            RenameColumn(table: "dbo.CemeteryTransactions", name: "TransferredCemeteryTransactionAF", newName: "TransferredPlotTransactionAF");
            RenameColumn(table: "dbo.Receipts", name: "CemeteryTransactionAF", newName: "PlotTransactionAF");
            RenameColumn(table: "dbo.PlotTrackings", name: "CemeteryTransactionAF", newName: "PlotTransactionAF");
            RenameColumn(table: "dbo.Invoices", name: "CemeteryTransactionAF", newName: "PlotTransactionAF");
            RenameTable(name: "dbo.CemeteryTransactions", newName: "PlotTransactions");
        }
    }
}
