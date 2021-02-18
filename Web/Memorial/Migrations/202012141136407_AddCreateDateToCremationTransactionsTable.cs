namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDateToCremationTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CremationTransactions", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CremationTransactions", "CreateDate");
        }
    }
}
