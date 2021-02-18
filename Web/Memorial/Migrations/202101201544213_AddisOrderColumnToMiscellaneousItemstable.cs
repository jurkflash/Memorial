namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisOrderColumnToMiscellaneousItemstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MiscellaneousItems", "isOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MiscellaneousItems", "isOrder");
        }
    }
}
