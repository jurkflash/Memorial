namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleTransactionsToColumbariumTransactionsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuadrangleTransactions", newName: "ColumbariumTransactions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ColumbariumTransactions", newName: "QuadrangleTransactions");
        }
    }
}
