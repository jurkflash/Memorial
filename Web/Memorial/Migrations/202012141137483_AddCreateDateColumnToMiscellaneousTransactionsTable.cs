namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDateColumnToMiscellaneousTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MiscellaneousTransactions", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MiscellaneousTransactions", "CreateDate");
        }
    }
}
