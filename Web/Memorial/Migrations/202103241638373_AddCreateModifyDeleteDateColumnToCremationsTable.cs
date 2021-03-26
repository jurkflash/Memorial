namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateColumnToCremationsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cremations", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cremations", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Cremations", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cremations", "DeleteDate");
            DropColumn("dbo.Cremations", "ModifyDate");
            DropColumn("dbo.Cremations", "CreateDate");
        }
    }
}
