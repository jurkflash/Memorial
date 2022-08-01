namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDeleteModifyDateColumnToAncestorQuadrangleTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestorTransactions", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.AncestorTransactions", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleTransactions", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleTransactions", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuadrangleTransactions", "DeleteDate");
            DropColumn("dbo.QuadrangleTransactions", "ModifyDate");
            DropColumn("dbo.AncestorTransactions", "DeleteDate");
            DropColumn("dbo.AncestorTransactions", "ModifyDate");
        }
    }
}
