namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModifyAndDeleteDateToCremationTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CremationTransactions", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.CremationTransactions", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CremationTransactions", "DeleteDate");
            DropColumn("dbo.CremationTransactions", "ModifyDate");
        }
    }
}
