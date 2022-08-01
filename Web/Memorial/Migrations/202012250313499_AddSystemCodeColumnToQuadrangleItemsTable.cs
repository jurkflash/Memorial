namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemCodeColumnToQuadrangleItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleItems", "SystemCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuadrangleItems", "SystemCode");
        }
    }
}
