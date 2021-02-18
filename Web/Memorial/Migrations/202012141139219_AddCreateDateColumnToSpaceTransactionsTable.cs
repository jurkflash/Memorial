namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDateColumnToSpaceTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpaceTransactions", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpaceTransactions", "CreateDate");
        }
    }
}
