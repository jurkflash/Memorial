namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpaceNumbersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpaceNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpaceItemId = c.Int(nullable: false),
                        Year = c.Byte(nullable: false),
                        AF = c.Byte(nullable: false),
                        PO = c.Byte(nullable: false),
                        IV = c.Byte(nullable: false),
                        RE = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SpaceItems", t => t.SpaceItemId)
                .Index(t => t.SpaceItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpaceNumbers", "SpaceItemId", "dbo.SpaceItems");
            DropIndex("dbo.SpaceNumbers", new[] { "SpaceItemId" });
            DropTable("dbo.SpaceNumbers");
        }
    }
}
