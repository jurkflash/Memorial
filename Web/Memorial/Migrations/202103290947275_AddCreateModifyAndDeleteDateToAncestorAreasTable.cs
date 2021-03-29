namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyAndDeleteDateToAncestorAreasTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestorAreas", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AncestorAreas", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.AncestorAreas", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AncestorAreas", "DeleteDate");
            DropColumn("dbo.AncestorAreas", "ModifyDate");
            DropColumn("dbo.AncestorAreas", "CreateDate");
        }
    }
}
