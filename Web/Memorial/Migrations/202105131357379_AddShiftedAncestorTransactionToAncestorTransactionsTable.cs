namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShiftedAncestorTransactionToAncestorTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestorTransactions", "ShiftedAncestorTransactionAF", c => c.String(maxLength: 50));
            CreateIndex("dbo.AncestorTransactions", "ShiftedAncestorTransactionAF");
            AddForeignKey("dbo.AncestorTransactions", "ShiftedAncestorTransactionAF", "dbo.AncestorTransactions", "AF");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AncestorTransactions", "ShiftedAncestorTransactionAF", "dbo.AncestorTransactions");
            DropIndex("dbo.AncestorTransactions", new[] { "ShiftedAncestorTransactionAF" });
            DropColumn("dbo.AncestorTransactions", "ShiftedAncestorTransactionAF");
        }
    }
}
