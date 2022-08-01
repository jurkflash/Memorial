namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransferredFromApplicantFieldToQuadrangleTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTransactions", "TransferredFromApplicantId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuadrangleTransactions", "TransferredFromApplicantId");
            AddForeignKey("dbo.QuadrangleTransactions", "TransferredFromApplicantId", "dbo.Applicants", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "TransferredFromApplicantId", "dbo.Applicants");
            DropIndex("dbo.QuadrangleTransactions", new[] { "TransferredFromApplicantId" });
            DropColumn("dbo.QuadrangleTransactions", "TransferredFromApplicantId");
        }
    }
}
