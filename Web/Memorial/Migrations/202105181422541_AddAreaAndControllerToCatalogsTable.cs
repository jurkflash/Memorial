namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAreaAndControllerToCatalogsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catalogs", "Area", c => c.String(maxLength: 255));
            AddColumn("dbo.Catalogs", "Controller", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catalogs", "Controller");
            DropColumn("dbo.Catalogs", "Area");
        }
    }
}
