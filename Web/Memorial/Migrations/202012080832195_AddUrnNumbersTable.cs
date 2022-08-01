namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrnNumbersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UrnNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrnItemId = c.Int(nullable: false),
                        Year = c.Byte(nullable: false),
                        AF = c.Byte(nullable: false),
                        PO = c.Byte(nullable: false),
                        IV = c.Byte(nullable: false),
                        RE = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UrnItems", t => t.UrnItemId)
                .Index(t => t.UrnItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UrnNumbers", "UrnItemId", "dbo.UrnItems");
            DropIndex("dbo.UrnNumbers", new[] { "UrnItemId" });
            DropTable("dbo.UrnNumbers");
        }
    }
}
