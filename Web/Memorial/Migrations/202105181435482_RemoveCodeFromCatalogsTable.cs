namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCodeFromCatalogsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Catalogs", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Catalogs", "Code", c => c.String());
        }
    }
}
