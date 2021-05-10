namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransferredApplicantAndTransactionToQuadrangleTransactionsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "TransferredFromApplicantId", "dbo.Applicants");
            AddColumn("dbo.QuadrangleTransactions", "TransferredApplicantId", c => c.Int());
            AddColumn("dbo.QuadrangleTransactions", "TransferredQuadrangleTransactionAF", c => c.String(maxLength: 50));
            CreateIndex("dbo.QuadrangleTransactions", "TransferredApplicantId");
            CreateIndex("dbo.QuadrangleTransactions", "TransferredQuadrangleTransactionAF");
            AddForeignKey("dbo.QuadrangleTransactions", "TransferredQuadrangleTransactionAF", "dbo.QuadrangleTransactions", "AF");
            AddForeignKey("dbo.QuadrangleTransactions", "TransferredApplicantId", "dbo.Applicants", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "TransferredApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.QuadrangleTransactions", "TransferredQuadrangleTransactionAF", "dbo.QuadrangleTransactions");
            DropIndex("dbo.QuadrangleTransactions", new[] { "TransferredQuadrangleTransactionAF" });
            DropIndex("dbo.QuadrangleTransactions", new[] { "TransferredApplicantId" });
            DropColumn("dbo.QuadrangleTransactions", "TransferredQuadrangleTransactionAF");
            DropColumn("dbo.QuadrangleTransactions", "TransferredApplicantId");
            AddForeignKey("dbo.QuadrangleTransactions", "TransferredFromApplicantId", "dbo.Applicants", "Id");
        }
    }
}
