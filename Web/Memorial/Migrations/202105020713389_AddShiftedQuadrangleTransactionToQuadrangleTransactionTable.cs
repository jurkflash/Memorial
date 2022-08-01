namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShiftedQuadrangleTransactionToQuadrangleTransactionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTransactions", "ShiftedQuadrangleTransactionAF", c => c.String(maxLength: 50));
            CreateIndex("dbo.QuadrangleTransactions", "ShiftedQuadrangleTransactionAF");
            AddForeignKey("dbo.QuadrangleTransactions", "ShiftedQuadrangleTransactionAF", "dbo.QuadrangleTransactions", "AF");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "ShiftedQuadrangleTransactionAF", "dbo.QuadrangleTransactions");
            DropIndex("dbo.QuadrangleTransactions", new[] { "ShiftedQuadrangleTransactionAF" });
            DropColumn("dbo.QuadrangleTransactions", "ShiftedQuadrangleTransactionAF");
        }
    }
}
