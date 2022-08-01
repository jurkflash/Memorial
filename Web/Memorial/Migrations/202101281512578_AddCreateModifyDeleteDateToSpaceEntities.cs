namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateToSpaceEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpaceTransactions", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.SpaceTransactions", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.SpaceItems", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.SpaceItems", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.SpaceItems", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpaceItems", "DeleteDate");
            DropColumn("dbo.SpaceItems", "ModifyDate");
            DropColumn("dbo.SpaceItems", "CreateDate");
            DropColumn("dbo.SpaceTransactions", "DeleteDate");
            DropColumn("dbo.SpaceTransactions", "ModifyDate");
        }
    }
}
