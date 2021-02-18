namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateColumnToCremationItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CremationItems", "CreateDate", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AddColumn("dbo.CremationItems", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.CremationItems", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CremationItems", "DeleteDate");
            DropColumn("dbo.CremationItems", "ModifyDate");
            DropColumn("dbo.CremationItems", "CreateDate");
        }
    }
}
