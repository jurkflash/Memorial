namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsOrderColumnToCremationItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CremationItems", "isOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CremationItems", "isOrder");
        }
    }
}
