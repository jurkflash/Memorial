namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrnItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UrnItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        UrnId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Urns", t => t.UrnId)
                .Index(t => t.UrnId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UrnItems", "UrnId", "dbo.Urns");
            DropIndex("dbo.UrnItems", new[] { "UrnId" });
            DropTable("dbo.UrnItems");
        }
    }
}
