namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModifyDateAndSetDeleteDateTimeToAllowNullForUrnTypeTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Urns", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.UrnItems", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.UrnTransactions", "ModifyDate", c => c.DateTime());
            AlterColumn("dbo.Urns", "DeleteDate", c => c.DateTime());
            AlterColumn("dbo.UrnItems", "DeleteDate", c => c.DateTime());
            AlterColumn("dbo.UrnTransactions", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UrnTransactions", "DeleteDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UrnItems", "DeleteDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Urns", "DeleteDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.UrnTransactions", "ModifyDate");
            DropColumn("dbo.UrnItems", "ModifyDate");
            DropColumn("dbo.Urns", "ModifyDate");
        }
    }
}
