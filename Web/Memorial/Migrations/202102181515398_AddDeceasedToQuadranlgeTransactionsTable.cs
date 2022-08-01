namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeceasedToQuadranlgeTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTransactions", "DeceasedId", c => c.Int());
            CreateIndex("dbo.QuadrangleTransactions", "DeceasedId");
            AddForeignKey("dbo.QuadrangleTransactions", "DeceasedId", "dbo.Deceaseds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "DeceasedId", "dbo.Deceaseds");
            DropIndex("dbo.QuadrangleTransactions", new[] { "DeceasedId" });
            DropColumn("dbo.QuadrangleTransactions", "DeceasedId");
        }
    }
}
