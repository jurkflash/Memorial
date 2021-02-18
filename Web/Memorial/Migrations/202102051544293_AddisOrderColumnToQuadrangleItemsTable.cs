namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisOrderColumnToQuadrangleItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleItems", "isOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuadrangleItems", "isOrder");
        }
    }
}
