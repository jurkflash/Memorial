namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaintenanceShiftedQuadrangleToQuadrangleTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId", c => c.Int());
            CreateIndex("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId");
            AddForeignKey("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId", "dbo.Quadrangles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId", "dbo.Quadrangles");
            DropIndex("dbo.QuadrangleTransactions", new[] { "MaintenanceShiftedQuadrangleId" });
            DropColumn("dbo.QuadrangleTransactions", "MaintenanceShiftedQuadrangleId");
        }
    }
}
