namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateToCatalog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catalogs", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Catalogs", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Catalogs", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catalogs", "DeleteDate");
            DropColumn("dbo.Catalogs", "ModifyDate");
            DropColumn("dbo.Catalogs", "CreateDate");
        }
    }
}
