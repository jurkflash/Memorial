namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemCodeColumnToAncestorItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestorItems", "SystemCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AncestorItems", "SystemCode");
        }
    }
}
