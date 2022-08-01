namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotTypesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlotTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        NumberOfPlacement = c.Byte(nullable: false),
                        isFengShuiPlot = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlotTypes");
        }
    }
}
