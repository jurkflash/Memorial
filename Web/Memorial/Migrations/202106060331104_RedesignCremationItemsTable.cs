namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedesignCremationItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE CremationItems");
            Sql("DBCC CHECKIDENT ('CremationItems', RESEED, 0);");

            AddColumn("dbo.CremationItems", "SubProductServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.CremationItems", "Price", c => c.Single());
            AlterColumn("dbo.CremationItems", "Code", c => c.String(maxLength: 10));
            AlterColumn("dbo.CremationItems", "isOrder", c => c.Boolean());
            CreateIndex("dbo.CremationItems", "SubProductServiceId");
            AddForeignKey("dbo.CremationItems", "SubProductServiceId", "dbo.SubProductServices", "Id");
            DropColumn("dbo.CremationItems", "Name");
            DropColumn("dbo.CremationItems", "Description");
            DropColumn("dbo.CremationItems", "SystemCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CremationItems", "SystemCode", c => c.String());
            AddColumn("dbo.CremationItems", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.CremationItems", "Name", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.CremationItems", "SubProductServiceId", "dbo.SubProductServices");
            DropIndex("dbo.CremationItems", new[] { "SubProductServiceId" });
            AlterColumn("dbo.CremationItems", "isOrder", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CremationItems", "Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.CremationItems", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.CremationItems", "SubProductServiceId");
        }
    }
}
