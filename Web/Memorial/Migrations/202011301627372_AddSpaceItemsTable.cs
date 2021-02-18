namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpaceItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpaceItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        AllowDeposit = c.Boolean(nullable: false),
                        AllowDoubleBook = c.Boolean(nullable: false),
                        ToleranceHour = c.Byte(nullable: false),
                        SpaceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Spaces", t => t.SpaceId)
                .Index(t => t.SpaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpaceItems", "SpaceId", "dbo.Spaces");
            DropIndex("dbo.SpaceItems", new[] { "SpaceId" });
            DropTable("dbo.SpaceItems");
        }
    }
}
