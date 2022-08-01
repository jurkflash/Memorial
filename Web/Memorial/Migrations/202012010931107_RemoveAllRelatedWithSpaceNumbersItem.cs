namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAllRelatedWithSpaceNumbersItem : DbMigration
    {
        public override void Up()
        {
            //Tracking lost on this

            //DropForeignKey("dbo.SpaceNumbers", "SpaceId", "dbo.Spaces");
            //DropIndex("dbo.SpaceNumbers", new[] { "SpaceId" });
            //DropTable("dbo.SpaceNumbers");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.SpaceNumbers",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            SpaceId = c.Int(nullable: false),
            //            Year = c.Byte(nullable: false),
            //            AF = c.Byte(nullable: false),
            //            PO = c.Byte(nullable: false),
            //            IV = c.Byte(nullable: false),
            //            RE = c.Byte(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateIndex("dbo.SpaceNumbers", "SpaceId");
            //AddForeignKey("dbo.SpaceNumbers", "SpaceId", "dbo.Spaces", "Id");
        }
    }
}
