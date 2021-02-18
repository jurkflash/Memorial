namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFromAndToDateToNullableColumnForSpaceTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SpaceTransactions", "FromDate", c => c.DateTime());
            AlterColumn("dbo.SpaceTransactions", "ToDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SpaceTransactions", "ToDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SpaceTransactions", "FromDate", c => c.DateTime(nullable: false));
        }
    }
}
