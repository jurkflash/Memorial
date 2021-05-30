namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWithdrewAFSAndWithdrewColumbariunApplicantIdToColumbariumTransactionsTableAndColumnForTracking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ColumbariumTrackings", "ToDeleteFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ColumbariumTransactions", "WithdrewAFS", c => c.String());
            AddColumn("dbo.ColumbariumTransactions", "WithdrewColumbariumApplicantId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ColumbariumTransactions", "WithdrewColumbariumApplicantId");
            DropColumn("dbo.ColumbariumTransactions", "WithdrewAFS");
            DropColumn("dbo.ColumbariumTrackings", "ToDeleteFlag");
        }
    }
}
