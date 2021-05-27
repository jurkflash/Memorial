namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherChargesColumnToSpaceTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpaceTransactions", "OtherCharges", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpaceTransactions", "OtherCharges");
        }
    }
}
