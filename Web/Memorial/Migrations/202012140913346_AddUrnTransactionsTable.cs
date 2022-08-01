namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrnTransactionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UrnTransactions",
                c => new
                    {
                        AF = c.String(nullable: false, maxLength: 50),
                        Price = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        UrnItemId = c.Int(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.UrnItems", t => t.UrnItemId)
                .Index(t => t.UrnItemId)
                .Index(t => t.ApplicantId);
            
            AddForeignKey("dbo.Invoices", "AF", "dbo.UrnTransactions", "AF");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UrnTransactions", "UrnItemId", "dbo.UrnItems");
            DropForeignKey("dbo.Invoices", "AF", "dbo.UrnTransactions");
            DropForeignKey("dbo.UrnTransactions", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.UrnTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.UrnTransactions", new[] { "UrnItemId" });
            DropTable("dbo.UrnTransactions");
        }
    }
}
