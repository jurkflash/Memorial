namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDateColumnToUrnTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrnTransactions", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrnTransactions", "CreateDate");
        }
    }
}
