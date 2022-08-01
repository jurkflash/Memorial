namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateToMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Miscellaneous", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Miscellaneous", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Miscellaneous", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Miscellaneous", "DeleteDate");
            DropColumn("dbo.Miscellaneous", "ModifyDate");
            DropColumn("dbo.Miscellaneous", "CreateDate");
        }
    }
}
