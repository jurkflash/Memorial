namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedesignSpaceItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE SpaceItems");
            Sql("DBCC CHECKIDENT ('SpaceItems', RESEED, 0);");

            AddColumn("dbo.SpaceItems", "SubProductServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.SpaceItems", "Price", c => c.Single());
            AlterColumn("dbo.SpaceItems", "Code", c => c.String(maxLength: 10));
            AlterColumn("dbo.SpaceItems", "isOrder", c => c.Boolean());
            CreateIndex("dbo.SpaceItems", "SubProductServiceId");
            AddForeignKey("dbo.SpaceItems", "SubProductServiceId", "dbo.SubProductServices", "Id");
            DropColumn("dbo.SpaceItems", "Name");
            DropColumn("dbo.SpaceItems", "Description");
            DropColumn("dbo.SpaceItems", "SystemCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpaceItems", "SystemCode", c => c.String());
            AddColumn("dbo.SpaceItems", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.SpaceItems", "Name", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.SpaceItems", "SubProductServiceId", "dbo.SubProductServices");
            DropIndex("dbo.SpaceItems", new[] { "SubProductServiceId" });
            AlterColumn("dbo.SpaceItems", "isOrder", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SpaceItems", "Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.SpaceItems", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.SpaceItems", "SubProductServiceId");
        }
    }
}
