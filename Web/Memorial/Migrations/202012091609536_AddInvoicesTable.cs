namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoicesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        IV = c.String(nullable: false, maxLength: 50),
                        AF = c.String(maxLength: 50),
                        Amount = c.Single(nullable: false),
                        isPaid = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IV)
                .ForeignKey("dbo.MiscellaneousTransactions", t => t.AF)
                .ForeignKey("dbo.SpaceTransactions", t => t.AF)
                .Index(t => t.AF);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "AF", "dbo.SpaceTransactions");
            DropForeignKey("dbo.Invoices", "AF", "dbo.MiscellaneousTransactions");
            DropIndex("dbo.Invoices", new[] { "AF" });
            DropTable("dbo.Invoices");
        }
    }
}
