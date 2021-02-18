namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemCodeColumnToUrnItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrnItems", "SystemCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrnItems", "SystemCode");
        }
    }
}
