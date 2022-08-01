namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransferredApplicantAndPlotTransactionToPlotTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotTransactions", "TransferredApplicantId", c => c.Int());
            AddColumn("dbo.PlotTransactions", "TransferredPlotTransactionAF", c => c.String(maxLength: 50));
            CreateIndex("dbo.PlotTransactions", "TransferredApplicantId");
            CreateIndex("dbo.PlotTransactions", "TransferredPlotTransactionAF");
            AddForeignKey("dbo.PlotTransactions", "TransferredApplicantId", "dbo.Applicants", "Id");
            AddForeignKey("dbo.PlotTransactions", "TransferredPlotTransactionAF", "dbo.PlotTransactions", "AF");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotTransactions", "TransferredPlotTransactionAF", "dbo.PlotTransactions");
            DropForeignKey("dbo.PlotTransactions", "TransferredApplicantId", "dbo.Applicants");
            DropIndex("dbo.PlotTransactions", new[] { "TransferredPlotTransactionAF" });
            DropIndex("dbo.PlotTransactions", new[] { "TransferredApplicantId" });
            DropColumn("dbo.PlotTransactions", "TransferredPlotTransactionAF");
            DropColumn("dbo.PlotTransactions", "TransferredApplicantId");
        }
    }
}
