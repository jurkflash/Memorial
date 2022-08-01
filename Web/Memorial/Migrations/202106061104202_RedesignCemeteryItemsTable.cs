namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedesignCemeteryItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE CemeteryItems");
            Sql("DBCC CHECKIDENT ('CemeteryItems', RESEED, 0);");

            AddColumn("dbo.CemeteryItems", "SubProductServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.CemeteryItems", "Price", c => c.Single());
            AlterColumn("dbo.CemeteryItems", "Code", c => c.String(maxLength: 10));
            AlterColumn("dbo.CemeteryItems", "isOrder", c => c.Boolean());
            CreateIndex("dbo.CemeteryItems", "SubProductServiceId");
            AddForeignKey("dbo.CemeteryItems", "SubProductServiceId", "dbo.SubProductServices", "Id");
            DropColumn("dbo.CemeteryItems", "Name");
            DropColumn("dbo.CemeteryItems", "Description");
            DropColumn("dbo.CemeteryItems", "SystemCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CemeteryItems", "SystemCode", c => c.String());
            AddColumn("dbo.CemeteryItems", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.CemeteryItems", "Name", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.CemeteryItems", "SubProductServiceId", "dbo.SubProductServices");
            DropIndex("dbo.CemeteryItems", new[] { "SubProductServiceId" });
            AlterColumn("dbo.CemeteryItems", "isOrder", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CemeteryItems", "Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.CemeteryItems", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.CemeteryItems", "SubProductServiceId");
        }
    }
}
