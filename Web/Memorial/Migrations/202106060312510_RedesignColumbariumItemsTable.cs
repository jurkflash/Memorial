namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedesignColumbariumItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE ColumbariumItems");
            Sql("DBCC CHECKIDENT ('ColumbariumItems', RESEED, 0);");

            AddColumn("dbo.ColumbariumItems", "SubProductServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.ColumbariumItems", "Price", c => c.Single());
            AlterColumn("dbo.ColumbariumItems", "Code", c => c.String(maxLength: 10));
            AlterColumn("dbo.ColumbariumItems", "isOrder", c => c.Boolean());
            CreateIndex("dbo.ColumbariumItems", "SubProductServiceId");
            AddForeignKey("dbo.ColumbariumItems", "SubProductServiceId", "dbo.SubProductServices", "Id");
            DropColumn("dbo.ColumbariumItems", "Name");
            DropColumn("dbo.ColumbariumItems", "Description");
            DropColumn("dbo.ColumbariumItems", "SystemCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ColumbariumItems", "SystemCode", c => c.String());
            AddColumn("dbo.ColumbariumItems", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.ColumbariumItems", "Name", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.ColumbariumItems", "SubProductServiceId", "dbo.SubProductServices");
            DropIndex("dbo.ColumbariumItems", new[] { "SubProductServiceId" });
            AlterColumn("dbo.ColumbariumItems", "isOrder", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ColumbariumItems", "Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.ColumbariumItems", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.ColumbariumItems", "SubProductServiceId");
        }
    }
}
