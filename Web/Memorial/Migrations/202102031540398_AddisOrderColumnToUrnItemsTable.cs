namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisOrderColumnToUrnItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrnItems", "isOrder", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrnItems", "isOrder");
        }
    }
}
