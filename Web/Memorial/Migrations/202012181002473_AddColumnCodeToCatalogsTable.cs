namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCodeToCatalogsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catalogs", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catalogs", "Code");
        }
    }
}
