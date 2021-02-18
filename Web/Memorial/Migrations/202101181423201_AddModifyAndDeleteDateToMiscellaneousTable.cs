namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModifyAndDeleteDateToMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MiscellaneousTransactions", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.MiscellaneousTransactions", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MiscellaneousTransactions", "DeleteDate");
            DropColumn("dbo.MiscellaneousTransactions", "ModifyDate");
        }
    }
}
