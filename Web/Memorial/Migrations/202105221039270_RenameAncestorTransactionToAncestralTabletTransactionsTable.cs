namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAncestorTransactionToAncestralTabletTransactionsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AncestorTransactions", newName: "AncestralTabletTransactions");
            RenameColumn(table: "dbo.AncestorTrackings", name: "AncestorTransactionAF", newName: "AncestralTabletTransactionAF");
            RenameColumn(table: "dbo.Invoices", name: "AncestorTransactionAF", newName: "AncestralTabletTransactionAF");
            RenameColumn(table: "dbo.Receipts", name: "AncestorTransactionAF", newName: "AncestralTabletTransactionAF");
            RenameColumn(table: "dbo.AncestralTabletTransactions", name: "ShiftedAncestorTransactionAF", newName: "ShiftedAncestralTabletTransactionAF");
            RenameIndex(table: "dbo.AncestralTabletTransactions", name: "IX_ShiftedAncestorTransactionAF", newName: "IX_ShiftedAncestralTabletTransactionAF");
            RenameIndex(table: "dbo.AncestorTrackings", name: "IX_AncestorTransactionAF", newName: "IX_AncestralTabletTransactionAF");
            RenameIndex(table: "dbo.Invoices", name: "IX_AncestorTransactionAF", newName: "IX_AncestralTabletTransactionAF");
            RenameIndex(table: "dbo.Receipts", name: "IX_AncestorTransactionAF", newName: "IX_AncestralTabletTransactionAF");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Receipts", name: "IX_AncestralTabletTransactionAF", newName: "IX_AncestorTransactionAF");
            RenameIndex(table: "dbo.Invoices", name: "IX_AncestralTabletTransactionAF", newName: "IX_AncestorTransactionAF");
            RenameIndex(table: "dbo.AncestorTrackings", name: "IX_AncestralTabletTransactionAF", newName: "IX_AncestorTransactionAF");
            RenameIndex(table: "dbo.AncestralTabletTransactions", name: "IX_ShiftedAncestralTabletTransactionAF", newName: "IX_ShiftedAncestorTransactionAF");
            RenameColumn(table: "dbo.AncestralTabletTransactions", name: "ShiftedAncestralTabletTransactionAF", newName: "ShiftedAncestorTransactionAF");
            RenameColumn(table: "dbo.Receipts", name: "AncestralTabletTransactionAF", newName: "AncestorTransactionAF");
            RenameColumn(table: "dbo.Invoices", name: "AncestralTabletTransactionAF", newName: "AncestorTransactionAF");
            RenameColumn(table: "dbo.AncestorTrackings", name: "AncestralTabletTransactionAF", newName: "AncestorTransactionAF");
            RenameTable(name: "dbo.AncestralTabletTransactions", newName: "AncestorTransactions");
        }
    }
}
