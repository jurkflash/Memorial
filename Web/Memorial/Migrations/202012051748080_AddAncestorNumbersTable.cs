namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAncestorNumbersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AncestorNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AncestorItemId = c.Int(nullable: false),
                        Year = c.Byte(nullable: false),
                        AF = c.Byte(nullable: false),
                        PO = c.Byte(nullable: false),
                        IV = c.Byte(nullable: false),
                        RE = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AncestorItems", t => t.AncestorItemId)
                .Index(t => t.AncestorItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AncestorNumbers", "AncestorItemId", "dbo.AncestorItems");
            DropIndex("dbo.AncestorNumbers", new[] { "AncestorItemId" });
            DropTable("dbo.AncestorNumbers");
        }
    }
}
