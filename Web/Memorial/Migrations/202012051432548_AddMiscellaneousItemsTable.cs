namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMiscellaneousItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MiscellaneousItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        MiscellaneousId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Miscellaneous", t => t.MiscellaneousId)
                .Index(t => t.MiscellaneousId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MiscellaneousItems", "MiscellaneousId", "dbo.Miscellaneous");
            DropIndex("dbo.MiscellaneousItems", new[] { "MiscellaneousId" });
            DropTable("dbo.MiscellaneousItems");
        }
    }
}
