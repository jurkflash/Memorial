namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMaintenanceQuadrangleIdFromQuadrangeTransactionsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId", "dbo.Quadrangles");
            DropIndex("dbo.QuadrangleTransactions", new[] { "MaintenanceShiftedQuadrangleId" });
            DropColumn("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId", c => c.Int());
            CreateIndex("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId");
            AddForeignKey("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId", "dbo.Quadrangles", "Id");
        }
    }
}
