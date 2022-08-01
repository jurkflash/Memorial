namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleToColumbariumTableColumns : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Invoices", name: "QuadrangleTransactionAF", newName: "ColumbariumTransactionAF");
            RenameColumn(table: "dbo.Receipts", name: "QuadrangleTransactionAF", newName: "ColumbariumTransactionAF");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "ShiftedQuadrangleTransactionAF", newName: "ShiftedColumbariumTransactionAF");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "TransferredQuadrangleTransactionAF", newName: "TransferredColumbariumTransactionAF");
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_ShiftedQuadrangleTransactionAF", newName: "IX_ShiftedColumbariumTransactionAF");
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_TransferredQuadrangleTransactionAF", newName: "IX_TransferredColumbariumTransactionAF");
            RenameIndex(table: "dbo.Invoices", name: "IX_QuadrangleTransactionAF", newName: "IX_ColumbariumTransactionAF");
            RenameIndex(table: "dbo.Receipts", name: "IX_QuadrangleTransactionAF", newName: "IX_ColumbariumTransactionAF");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Receipts", name: "IX_ColumbariumTransactionAF", newName: "IX_QuadrangleTransactionAF");
            RenameIndex(table: "dbo.Invoices", name: "IX_ColumbariumTransactionAF", newName: "IX_QuadrangleTransactionAF");
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_TransferredColumbariumTransactionAF", newName: "IX_TransferredQuadrangleTransactionAF");
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_ShiftedColumbariumTransactionAF", newName: "IX_ShiftedQuadrangleTransactionAF");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "TransferredColumbariumTransactionAF", newName: "TransferredQuadrangleTransactionAF");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "ShiftedColumbariumTransactionAF", newName: "ShiftedQuadrangleTransactionAF");
            RenameColumn(table: "dbo.Receipts", name: "ColumbariumTransactionAF", newName: "QuadrangleTransactionAF");
            RenameColumn(table: "dbo.Invoices", name: "ColumbariumTransactionAF", newName: "QuadrangleTransactionAF");
        }
    }
}
