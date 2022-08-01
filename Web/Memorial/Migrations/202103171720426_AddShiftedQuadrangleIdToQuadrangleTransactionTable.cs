namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShiftedQuadrangleIdToQuadrangleTransactionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTransactions", "ShiftedQuadrangleId", c => c.Int());
            CreateIndex("dbo.QuadrangleTransactions", "ShiftedQuadrangleId");
            AddForeignKey("dbo.QuadrangleTransactions", "ShiftedQuadrangleId", "dbo.Quadrangles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "ShiftedQuadrangleId", "dbo.Quadrangles");
            DropIndex("dbo.QuadrangleTransactions", new[] { "ShiftedQuadrangleId" });
            DropColumn("dbo.QuadrangleTransactions", "ShiftedQuadrangleId");
        }
    }
}
