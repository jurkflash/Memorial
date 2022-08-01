namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemCodeColumnToCremationItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CremationItems", "SystemCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CremationItems", "SystemCode");
        }
    }
}
