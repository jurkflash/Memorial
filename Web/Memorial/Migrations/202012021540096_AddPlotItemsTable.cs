namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlotItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        PlotAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlotAreas", t => t.PlotAreaId)
                .Index(t => t.PlotAreaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotItems", "PlotAreaId", "dbo.PlotAreas");
            DropIndex("dbo.PlotItems", new[] { "PlotAreaId" });
            DropTable("dbo.PlotItems");
        }
    }
}
