namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSummaryItemForAllTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestralTabletTransactions", "SummaryItem", c => c.String());
            AddColumn("dbo.CemeteryTransactions", "SummaryItem", c => c.String());
            AddColumn("dbo.ColumbariumTransactions", "SummaryItem", c => c.String());
            AddColumn("dbo.MiscellaneousTransactions", "SummaryItem", c => c.String());
            AddColumn("dbo.SpaceTransactions", "SummaryItem", c => c.String());
            AddColumn("dbo.UrnTransactions", "SummaryItem", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrnTransactions", "SummaryItem");
            DropColumn("dbo.SpaceTransactions", "SummaryItem");
            DropColumn("dbo.MiscellaneousTransactions", "SummaryItem");
            DropColumn("dbo.ColumbariumTransactions", "SummaryItem");
            DropColumn("dbo.CemeteryTransactions", "SummaryItem");
            DropColumn("dbo.AncestralTabletTransactions", "SummaryItem");
        }
    }
}
