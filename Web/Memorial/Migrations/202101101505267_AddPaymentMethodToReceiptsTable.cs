namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentMethodToReceiptsTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Receipts", new[] { "InvoiceIV" });
            AddColumn("dbo.Receipts", "PaymentMethodId", c => c.Byte(nullable: false));
            AddColumn("dbo.Receipts", "PaymentRemark", c => c.String());
            AlterColumn("dbo.Receipts", "InvoiceIV", c => c.String(maxLength: 50));
            CreateIndex("dbo.Receipts", "InvoiceIV");
            CreateIndex("dbo.Receipts", "PaymentMethodId");
            AddForeignKey("dbo.Receipts", "PaymentMethodId", "dbo.PaymentMethods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receipts", "PaymentMethodId", "dbo.PaymentMethods");
            DropIndex("dbo.Receipts", new[] { "PaymentMethodId" });
            DropIndex("dbo.Receipts", new[] { "InvoiceIV" });
            AlterColumn("dbo.Receipts", "InvoiceIV", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Receipts", "PaymentRemark");
            DropColumn("dbo.Receipts", "PaymentMethodId");
            CreateIndex("dbo.Receipts", "InvoiceIV");
        }
    }
}
