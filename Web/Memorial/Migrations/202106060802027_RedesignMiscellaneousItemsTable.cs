namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedesignMiscellaneousItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE MiscellaneousItems");
            Sql("DBCC CHECKIDENT ('MiscellaneousItems', RESEED, 0);");

            AddColumn("dbo.MiscellaneousItems", "SubProductServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.MiscellaneousItems", "Price", c => c.Single());
            AlterColumn("dbo.MiscellaneousItems", "Code", c => c.String(maxLength: 10));
            AlterColumn("dbo.MiscellaneousItems", "isOrder", c => c.Boolean());
            CreateIndex("dbo.MiscellaneousItems", "SubProductServiceId");
            AddForeignKey("dbo.MiscellaneousItems", "SubProductServiceId", "dbo.SubProductServices", "Id");
            DropColumn("dbo.MiscellaneousItems", "Name");
            DropColumn("dbo.MiscellaneousItems", "Description");
            DropColumn("dbo.MiscellaneousItems", "SystemCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MiscellaneousItems", "SystemCode", c => c.String());
            AddColumn("dbo.MiscellaneousItems", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.MiscellaneousItems", "Name", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.MiscellaneousItems", "SubProductServiceId", "dbo.SubProductServices");
            DropIndex("dbo.MiscellaneousItems", new[] { "SubProductServiceId" });
            AlterColumn("dbo.MiscellaneousItems", "isOrder", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MiscellaneousItems", "Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.MiscellaneousItems", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.MiscellaneousItems", "SubProductServiceId");
        }
    }
}
