namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisOrderColumnToAncestorItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestorItems", "isOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AncestorItems", "isOrder");
        }
    }
}
