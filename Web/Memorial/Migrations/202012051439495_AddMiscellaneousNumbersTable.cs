namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMiscellaneousNumbersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MiscellaneousNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MiscellaneousItemId = c.Int(nullable: false),
                        Year = c.Byte(nullable: false),
                        AF = c.Byte(nullable: false),
                        PO = c.Byte(nullable: false),
                        IV = c.Byte(nullable: false),
                        RE = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MiscellaneousItems", t => t.MiscellaneousItemId)
                .Index(t => t.MiscellaneousItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MiscellaneousNumbers", "MiscellaneousItemId", "dbo.MiscellaneousItems");
            DropIndex("dbo.MiscellaneousNumbers", new[] { "MiscellaneousItemId" });
            DropTable("dbo.MiscellaneousNumbers");
        }
    }
}
