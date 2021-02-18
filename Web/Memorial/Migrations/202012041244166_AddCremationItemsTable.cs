namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCremationItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CremationItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        CremationId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cremations", t => t.CremationId)
                .Index(t => t.CremationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CremationItems", "CremationId", "dbo.Cremations");
            DropIndex("dbo.CremationItems", new[] { "CremationId" });
            DropTable("dbo.CremationItems");
        }
    }
}
