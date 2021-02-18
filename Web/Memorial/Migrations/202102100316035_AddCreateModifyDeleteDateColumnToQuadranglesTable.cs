namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateColumnToQuadranglesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quadrangles", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quadrangles", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Quadrangles", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quadrangles", "DeleteDate");
            DropColumn("dbo.Quadrangles", "ModifyDate");
            DropColumn("dbo.Quadrangles", "CreateDate");
        }
    }
}
