namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotNumbersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlotNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlotItemId = c.Int(nullable: false),
                        Year = c.Byte(nullable: false),
                        AF = c.Byte(nullable: false),
                        PO = c.Byte(nullable: false),
                        IV = c.Byte(nullable: false),
                        RE = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlotItems", t => t.PlotItemId)
                .Index(t => t.PlotItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotNumbers", "PlotItemId", "dbo.PlotItems");
            DropIndex("dbo.PlotNumbers", new[] { "PlotItemId" });
            DropTable("dbo.PlotNumbers");
        }
    }
}
