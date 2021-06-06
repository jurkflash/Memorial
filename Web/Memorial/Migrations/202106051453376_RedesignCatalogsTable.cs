namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedesignCatalogsTable : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE Catalogs");

            AddColumn("dbo.Catalogs", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Catalogs", "ProductId");
            AddForeignKey("dbo.Catalogs", "ProductId", "dbo.Products", "Id");
            DropColumn("dbo.Catalogs", "Name");
            DropColumn("dbo.Catalogs", "Description");
            DropColumn("dbo.Catalogs", "Area");
            DropColumn("dbo.Catalogs", "Controller");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Catalogs", "Controller", c => c.String(maxLength: 255));
            AddColumn("dbo.Catalogs", "Area", c => c.String(maxLength: 255));
            AddColumn("dbo.Catalogs", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.Catalogs", "Name", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.Catalogs", "ProductId", "dbo.Products");
            DropIndex("dbo.Catalogs", new[] { "ProductId" });
            DropColumn("dbo.Catalogs", "ProductId");
        }
    }
}
