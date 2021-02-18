namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemCodeColumnToMiscellaneousItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MiscellaneousItems", "SystemCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MiscellaneousItems", "SystemCode");
        }
    }
}
