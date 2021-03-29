namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFromToDateAndShiftedAncestorIdToAncestorTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestorTransactions", "FromDate", c => c.DateTime());
            AddColumn("dbo.AncestorTransactions", "ToDate", c => c.DateTime());
            AddColumn("dbo.AncestorTransactions", "ShiftedAncestorId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AncestorTransactions", "ShiftedAncestorId");
            DropColumn("dbo.AncestorTransactions", "ToDate");
            DropColumn("dbo.AncestorTransactions", "FromDate");
        }
    }
}
