namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTransferredFormApplicantToAllowNullForQuadrangleTransactionsTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.QuadrangleTransactions", new[] { "TransferredFromApplicantId" });
            AlterColumn("dbo.QuadrangleTransactions", "TransferredFromApplicantId", c => c.Int());
            CreateIndex("dbo.QuadrangleTransactions", "TransferredFromApplicantId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuadrangleTransactions", new[] { "TransferredFromApplicantId" });
            AlterColumn("dbo.QuadrangleTransactions", "TransferredFromApplicantId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuadrangleTransactions", "TransferredFromApplicantId");
        }
    }
}
