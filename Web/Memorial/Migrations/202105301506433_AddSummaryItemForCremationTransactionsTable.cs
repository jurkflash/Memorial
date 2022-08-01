namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSummaryItemForCremationTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CremationTransactions", "SummaryItem", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CremationTransactions", "SummaryItem");
        }
    }
}
