namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsOrderColumnToSpaceItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpaceItems", "isOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpaceItems", "isOrder");
        }
    }
}
