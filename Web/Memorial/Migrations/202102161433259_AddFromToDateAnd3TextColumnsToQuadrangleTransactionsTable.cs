namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFromToDateAnd3TextColumnsToQuadrangleTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTransactions", "FromDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleTransactions", "ToDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleTransactions", "Text1", c => c.String());
            AddColumn("dbo.QuadrangleTransactions", "Text2", c => c.String());
            AddColumn("dbo.QuadrangleTransactions", "Text3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuadrangleTransactions", "Text3");
            DropColumn("dbo.QuadrangleTransactions", "Text2");
            DropColumn("dbo.QuadrangleTransactions", "Text1");
            DropColumn("dbo.QuadrangleTransactions", "ToDate");
            DropColumn("dbo.QuadrangleTransactions", "FromDate");
        }
    }
}
