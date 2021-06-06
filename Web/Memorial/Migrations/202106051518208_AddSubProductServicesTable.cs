namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubProductServicesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubProductServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Price = c.String(),
                        Code = c.String(nullable: false, maxLength: 10),
                        SystemCode = c.String(nullable: false, maxLength: 50),
                        isOrder = c.Boolean(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubProductServices", "ProductId", "dbo.Products");
            DropIndex("dbo.SubProductServices", new[] { "ProductId" });
            DropTable("dbo.SubProductServices");
        }
    }
}
