namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplicantIdToAllowNullForMiscellaneousTransactionsTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MiscellaneousTransactions", new[] { "ApplicantId" });
            AlterColumn("dbo.MiscellaneousTransactions", "ApplicantId", c => c.Int());
            CreateIndex("dbo.MiscellaneousTransactions", "ApplicantId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MiscellaneousTransactions", new[] { "ApplicantId" });
            AlterColumn("dbo.MiscellaneousTransactions", "ApplicantId", c => c.Int(nullable: false));
            CreateIndex("dbo.MiscellaneousTransactions", "ApplicantId");
        }
    }
}
