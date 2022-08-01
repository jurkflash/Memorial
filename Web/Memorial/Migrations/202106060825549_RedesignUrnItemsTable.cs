namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedesignUrnItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE UrnItems");
            Sql("DBCC CHECKIDENT ('UrnItems', RESEED, 0);");

            AddColumn("dbo.UrnItems", "SubProductServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.UrnItems", "Price", c => c.Single());
            AlterColumn("dbo.UrnItems", "Code", c => c.String(maxLength: 10));
            AlterColumn("dbo.UrnItems", "isOrder", c => c.Boolean());
            CreateIndex("dbo.UrnItems", "SubProductServiceId");
            AddForeignKey("dbo.UrnItems", "SubProductServiceId", "dbo.SubProductServices", "Id");
            DropColumn("dbo.UrnItems", "Name");
            DropColumn("dbo.UrnItems", "Description");
            DropColumn("dbo.UrnItems", "SystemCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UrnItems", "SystemCode", c => c.String());
            AddColumn("dbo.UrnItems", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.UrnItems", "Name", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.UrnItems", "SubProductServiceId", "dbo.SubProductServices");
            DropIndex("dbo.UrnItems", new[] { "SubProductServiceId" });
            AlterColumn("dbo.UrnItems", "isOrder", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UrnItems", "Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.UrnItems", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.UrnItems", "SubProductServiceId");
        }
    }
}
