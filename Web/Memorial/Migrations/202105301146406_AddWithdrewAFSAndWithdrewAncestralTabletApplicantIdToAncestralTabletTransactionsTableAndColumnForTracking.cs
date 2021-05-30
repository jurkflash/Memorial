namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWithdrewAFSAndWithdrewAncestralTabletApplicantIdToAncestralTabletTransactionsTableAndColumnForTracking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestralTabletTransactions", "WithdrewAFS", c => c.String());
            AddColumn("dbo.AncestralTabletTransactions", "WithdrewAncestralTabletApplicantId", c => c.Int());
            AddColumn("dbo.AncestralTabletTrackings", "ToDeleteFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AncestralTabletTrackings", "ToDeleteFlag");
            DropColumn("dbo.AncestralTabletTransactions", "WithdrewAncestralTabletApplicantId");
            DropColumn("dbo.AncestralTabletTransactions", "WithdrewAFS");
        }
    }
}
