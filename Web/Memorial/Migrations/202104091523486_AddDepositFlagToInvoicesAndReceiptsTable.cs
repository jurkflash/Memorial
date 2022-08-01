namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepositFlagToInvoicesAndReceiptsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "AllowDeposit", c => c.Boolean(nullable: false));
            AddColumn("dbo.Receipts", "isDeposit", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Receipts", "isDeposit");
            DropColumn("dbo.Invoices", "AllowDeposit");
        }
    }
}
