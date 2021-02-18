namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetColumnAFToNotNullAndLength_RemarkLengthForSpaceAndMiscellaneousTransactionsTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MiscellaneousTransactions");
            DropPrimaryKey("dbo.SpaceTransactions");
            AlterColumn("dbo.MiscellaneousTransactions", "AF", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.MiscellaneousTransactions", "Remark", c => c.String(maxLength: 255));
            AlterColumn("dbo.SpaceTransactions", "AF", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.SpaceTransactions", "Remark", c => c.String(maxLength: 255));
            AddPrimaryKey("dbo.MiscellaneousTransactions", "AF");
            AddPrimaryKey("dbo.SpaceTransactions", "AF");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SpaceTransactions");
            DropPrimaryKey("dbo.MiscellaneousTransactions");
            AlterColumn("dbo.SpaceTransactions", "Remark", c => c.String());
            AlterColumn("dbo.SpaceTransactions", "AF", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.MiscellaneousTransactions", "Remark", c => c.String());
            AlterColumn("dbo.MiscellaneousTransactions", "AF", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.SpaceTransactions", "AF");
            AddPrimaryKey("dbo.MiscellaneousTransactions", "AF");
        }
    }
}
