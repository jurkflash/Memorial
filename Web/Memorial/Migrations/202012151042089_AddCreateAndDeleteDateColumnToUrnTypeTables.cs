namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateAndDeleteDateColumnToUrnTypeTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Urns", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Urns", "DeleteDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UrnItems", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UrnItems", "DeleteDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UrnTransactions", "DeleteDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrnTransactions", "DeleteDate");
            DropColumn("dbo.UrnItems", "DeleteDate");
            DropColumn("dbo.UrnItems", "CreateDate");
            DropColumn("dbo.Urns", "DeleteDate");
            DropColumn("dbo.Urns", "CreateDate");
        }
    }
}
