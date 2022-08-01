namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoiceAndReceiptForeignKeyToCremationTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Invoices", "AF", "dbo.CremationTransactions", "AF");
            AddForeignKey("dbo.Receipts", "AF", "dbo.CremationTransactions", "AF");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receipts", "AF", "dbo.CremationTransactions");
            DropForeignKey("dbo.Invoices", "AF", "dbo.CremationTransactions");
        }
    }
}
