namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuadrangleItemIdToQuadrangleTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTransactions", "QuadrangleItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuadrangleTransactions", "QuadrangleItemId");
            AddForeignKey("dbo.QuadrangleTransactions", "QuadrangleItemId", "dbo.QuadrangleItems", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "QuadrangleItemId", "dbo.QuadrangleItems");
            DropIndex("dbo.QuadrangleTransactions", new[] { "QuadrangleItemId" });
            DropColumn("dbo.QuadrangleTransactions", "QuadrangleItemId");
        }
    }
}
