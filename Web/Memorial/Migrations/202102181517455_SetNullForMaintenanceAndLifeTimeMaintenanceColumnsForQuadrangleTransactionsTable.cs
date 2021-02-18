namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNullForMaintenanceAndLifeTimeMaintenanceColumnsForQuadrangleTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuadrangleTransactions", "Maintenance", c => c.Single());
            AlterColumn("dbo.QuadrangleTransactions", "LifeTimeMaintenance", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuadrangleTransactions", "LifeTimeMaintenance", c => c.Single(nullable: false));
            AlterColumn("dbo.QuadrangleTransactions", "Maintenance", c => c.Single(nullable: false));
        }
    }
}
