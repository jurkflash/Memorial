namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeceased2ToQuadrangleTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTransactions", "Deceased2Id", c => c.Int());
            CreateIndex("dbo.QuadrangleTransactions", "Deceased2Id");
            AddForeignKey("dbo.QuadrangleTransactions", "Deceased2Id", "dbo.Deceaseds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "Deceased2Id", "dbo.Deceaseds");
            DropIndex("dbo.QuadrangleTransactions", new[] { "Deceased2Id" });
            DropColumn("dbo.QuadrangleTransactions", "Deceased2Id");
        }
    }
}
