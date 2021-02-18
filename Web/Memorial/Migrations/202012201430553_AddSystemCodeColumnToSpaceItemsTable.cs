namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemCodeColumnToSpaceItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpaceItems", "SystemCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpaceItems", "SystemCode");
        }
    }
}
