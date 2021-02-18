namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoiceAndReceiptForeignKeyToAllTransactionsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Receipts", "AF", "dbo.MiscellaneousTransactions");
            DropForeignKey("dbo.Receipts", "AF", "dbo.CremationTransactions");
            DropForeignKey("dbo.Invoices", "AF", "dbo.CremationTransactions");
            DropForeignKey("dbo.Invoices", "AF", "dbo.MiscellaneousTransactions");
            DropForeignKey("dbo.Invoices", "AF", "dbo.SpaceTransactions");
            DropForeignKey("dbo.Invoices", "AF", "dbo.UrnTransactions");
            DropIndex("dbo.Invoices", new[] { "AF" });
            DropIndex("dbo.Receipts", new[] { "AF" });
            DropIndex("dbo.Receipts", new[] { "IV" });
            RenameColumn(table: "dbo.Receipts", name: "IV", newName: "InvoiceIV");
            AddColumn("dbo.Invoices", "PlotTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "CremationTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "AncestorTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "MiscellaneousTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "QuadrangleTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "SpaceTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "UrnTransactionAF", c => c.String(maxLength: 50));
            AlterColumn("dbo.Invoices", "AF", c => c.String());
            AlterColumn("dbo.Receipts", "InvoiceIV", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Invoices", "PlotTransactionAF");
            CreateIndex("dbo.Invoices", "CremationTransactionAF");
            CreateIndex("dbo.Invoices", "AncestorTransactionAF");
            CreateIndex("dbo.Invoices", "MiscellaneousTransactionAF");
            CreateIndex("dbo.Invoices", "QuadrangleTransactionAF");
            CreateIndex("dbo.Invoices", "SpaceTransactionAF");
            CreateIndex("dbo.Invoices", "UrnTransactionAF");
            CreateIndex("dbo.Receipts", "InvoiceIV");
            AddForeignKey("dbo.Invoices", "AncestorTransactionAF", "dbo.AncestorTransactions", "AF");
            AddForeignKey("dbo.Invoices", "PlotTransactionAF", "dbo.PlotTransactions", "AF");
            AddForeignKey("dbo.Invoices", "QuadrangleTransactionAF", "dbo.QuadrangleTransactions", "AF");
            AddForeignKey("dbo.Invoices", "CremationTransactionAF", "dbo.CremationTransactions", "AF");
            AddForeignKey("dbo.Invoices", "MiscellaneousTransactionAF", "dbo.MiscellaneousTransactions", "AF");
            AddForeignKey("dbo.Invoices", "SpaceTransactionAF", "dbo.SpaceTransactions", "AF");
            AddForeignKey("dbo.Invoices", "UrnTransactionAF", "dbo.UrnTransactions", "AF");
            DropColumn("dbo.Receipts", "AF");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Receipts", "AF", c => c.String(maxLength: 50));
            DropForeignKey("dbo.Invoices", "UrnTransactionAF", "dbo.UrnTransactions");
            DropForeignKey("dbo.Invoices", "SpaceTransactionAF", "dbo.SpaceTransactions");
            DropForeignKey("dbo.Invoices", "MiscellaneousTransactionAF", "dbo.MiscellaneousTransactions");
            DropForeignKey("dbo.Invoices", "CremationTransactionAF", "dbo.CremationTransactions");
            DropForeignKey("dbo.Invoices", "QuadrangleTransactionAF", "dbo.QuadrangleTransactions");
            DropForeignKey("dbo.Invoices", "PlotTransactionAF", "dbo.PlotTransactions");
            DropForeignKey("dbo.Invoices", "AncestorTransactionAF", "dbo.AncestorTransactions");
            DropIndex("dbo.Receipts", new[] { "InvoiceIV" });
            DropIndex("dbo.Invoices", new[] { "UrnTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "SpaceTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "QuadrangleTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "MiscellaneousTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "AncestorTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "CremationTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "PlotTransactionAF" });
            AlterColumn("dbo.Receipts", "InvoiceIV", c => c.String(maxLength: 50));
            AlterColumn("dbo.Invoices", "AF", c => c.String(maxLength: 50));
            DropColumn("dbo.Invoices", "UrnTransactionAF");
            DropColumn("dbo.Invoices", "SpaceTransactionAF");
            DropColumn("dbo.Invoices", "QuadrangleTransactionAF");
            DropColumn("dbo.Invoices", "MiscellaneousTransactionAF");
            DropColumn("dbo.Invoices", "AncestorTransactionAF");
            DropColumn("dbo.Invoices", "CremationTransactionAF");
            DropColumn("dbo.Invoices", "PlotTransactionAF");
            RenameColumn(table: "dbo.Receipts", name: "InvoiceIV", newName: "IV");
            CreateIndex("dbo.Receipts", "IV");
            CreateIndex("dbo.Receipts", "AF");
            CreateIndex("dbo.Invoices", "AF");
            AddForeignKey("dbo.Invoices", "AF", "dbo.UrnTransactions", "AF");
            AddForeignKey("dbo.Invoices", "AF", "dbo.SpaceTransactions", "AF");
            AddForeignKey("dbo.Invoices", "AF", "dbo.MiscellaneousTransactions", "AF");
            AddForeignKey("dbo.Invoices", "AF", "dbo.CremationTransactions", "AF");
            AddForeignKey("dbo.Receipts", "AF", "dbo.CremationTransactions", "AF");
            AddForeignKey("dbo.Receipts", "AF", "dbo.MiscellaneousTransactions", "AF");
        }
    }
}
