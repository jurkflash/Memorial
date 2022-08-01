namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReduceLengthForColumnAFTo50ForPlotTransactionsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "ApplicantId", "dbo.Applicants");
            DropPrimaryKey("dbo.QuadrangleTransactions");
            AlterColumn("dbo.QuadrangleTransactions", "AF", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.QuadrangleTransactions", "AF");
            AddForeignKey("dbo.QuadrangleTransactions", "ApplicantId", "dbo.Applicants", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "ApplicantId", "dbo.Applicants");
            DropPrimaryKey("dbo.QuadrangleTransactions");
            AlterColumn("dbo.QuadrangleTransactions", "AF", c => c.String(nullable: false, maxLength: 255));
            AddPrimaryKey("dbo.QuadrangleTransactions", "AF");
            AddForeignKey("dbo.QuadrangleTransactions", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
        }
    }
}
