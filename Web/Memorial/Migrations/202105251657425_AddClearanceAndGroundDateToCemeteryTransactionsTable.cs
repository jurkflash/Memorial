namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClearanceAndGroundDateToCemeteryTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CemeteryTransactions", "ClearanceDate", c => c.DateTime());
            AddColumn("dbo.CemeteryTransactions", "ClearanceGroundDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CemeteryTransactions", "ClearanceGroundDate");
            DropColumn("dbo.CemeteryTransactions", "ClearanceDate");
        }
    }
}
