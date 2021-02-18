namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAncestorItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AncestorItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        AncestorAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AncestorAreas", t => t.AncestorAreaId)
                .Index(t => t.AncestorAreaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AncestorItems", "AncestorAreaId", "dbo.AncestorAreas");
            DropIndex("dbo.AncestorItems", new[] { "AncestorAreaId" });
            DropTable("dbo.AncestorItems");
        }
    }
}
