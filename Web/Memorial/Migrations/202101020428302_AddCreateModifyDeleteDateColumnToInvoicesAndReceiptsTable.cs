namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateColumnToInvoicesAndReceiptsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invoices", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Invoices", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.Receipts", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Receipts", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Receipts", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Receipts", "DeleteDate");
            DropColumn("dbo.Receipts", "ModifyDate");
            DropColumn("dbo.Receipts", "CreateDate");
            DropColumn("dbo.Invoices", "DeleteDate");
            DropColumn("dbo.Invoices", "ModifyDate");
            DropColumn("dbo.Invoices", "CreateDate");
        }
    }
}
