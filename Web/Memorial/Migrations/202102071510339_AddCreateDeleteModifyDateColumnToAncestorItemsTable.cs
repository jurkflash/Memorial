namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDeleteModifyDateColumnToAncestorItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestorItems", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AncestorItems", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.AncestorItems", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AncestorItems", "DeleteDate");
            DropColumn("dbo.AncestorItems", "ModifyDate");
            DropColumn("dbo.AncestorItems", "CreateDate");
        }
    }
}
