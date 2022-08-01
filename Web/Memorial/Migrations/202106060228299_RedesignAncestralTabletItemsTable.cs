namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedesignAncestralTabletItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE AncestralTabletItems");
            Sql("DBCC CHECKIDENT ('AncestralTabletItems', RESEED, 0);");

            AddColumn("dbo.AncestralTabletItems", "SubProductServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.AncestralTabletItems", "Price", c => c.Single());
            AlterColumn("dbo.AncestralTabletItems", "isOrder", c => c.Boolean());
            AlterColumn("dbo.SubProductServices", "Price", c => c.Single(nullable: false));
            CreateIndex("dbo.AncestralTabletItems", "SubProductServiceId");
            AddForeignKey("dbo.AncestralTabletItems", "SubProductServiceId", "dbo.SubProductServices", "Id");
            DropColumn("dbo.AncestralTabletItems", "Name");
            DropColumn("dbo.AncestralTabletItems", "Description");
            DropColumn("dbo.AncestralTabletItems", "SystemCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AncestralTabletItems", "SystemCode", c => c.String());
            AddColumn("dbo.AncestralTabletItems", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.AncestralTabletItems", "Name", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.AncestralTabletItems", "SubProductServiceId", "dbo.SubProductServices");
            DropIndex("dbo.AncestralTabletItems", new[] { "SubProductServiceId" });
            AlterColumn("dbo.SubProductServices", "Price", c => c.String());
            AlterColumn("dbo.AncestralTabletItems", "isOrder", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AncestralTabletItems", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.AncestralTabletItems", "SubProductServiceId");
        }
    }
}
