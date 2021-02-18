namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateToMiscellaneousItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MiscellaneousItems", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MiscellaneousItems", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.MiscellaneousItems", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MiscellaneousItems", "DeleteDate");
            DropColumn("dbo.MiscellaneousItems", "ModifyDate");
            DropColumn("dbo.MiscellaneousItems", "CreateDate");
        }
    }
}
