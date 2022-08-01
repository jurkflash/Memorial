namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateToSpacesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Spaces", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Spaces", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Spaces", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Spaces", "DeleteDate");
            DropColumn("dbo.Spaces", "ModifyDate");
            DropColumn("dbo.Spaces", "CreateDate");
        }
    }
}
