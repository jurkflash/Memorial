namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCremationNumbersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CremationNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CremationItemId = c.Int(nullable: false),
                        Year = c.Byte(nullable: false),
                        AF = c.Byte(nullable: false),
                        PO = c.Byte(nullable: false),
                        IV = c.Byte(nullable: false),
                        RE = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CremationItems", t => t.CremationItemId)
                .Index(t => t.CremationItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CremationNumbers", "CremationItemId", "dbo.CremationItems");
            DropIndex("dbo.CremationNumbers", new[] { "CremationItemId" });
            DropTable("dbo.CremationNumbers");
        }
    }
}
