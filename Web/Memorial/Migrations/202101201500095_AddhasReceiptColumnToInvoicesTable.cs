namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhasReceiptColumnToInvoicesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "hasReceipt", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "hasReceipt");
        }
    }
}
