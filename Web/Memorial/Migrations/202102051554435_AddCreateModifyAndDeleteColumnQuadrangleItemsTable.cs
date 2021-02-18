namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyAndDeleteColumnQuadrangleItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleItems", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuadrangleItems", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleItems", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuadrangleItems", "DeleteDate");
            DropColumn("dbo.QuadrangleItems", "ModifyDate");
            DropColumn("dbo.QuadrangleItems", "CreateDate");
        }
    }
}
