namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkMiscellaneousTransactionsAFColumnWIthReceiptsTable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Receipts", "AF");
            AddForeignKey("dbo.Receipts", "AF", "dbo.MiscellaneousTransactions", "AF");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receipts", "AF", "dbo.MiscellaneousTransactions");
            DropIndex("dbo.Receipts", new[] { "AF" });
        }
    }
}
