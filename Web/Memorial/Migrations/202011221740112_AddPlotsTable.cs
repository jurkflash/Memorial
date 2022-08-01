namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        Size = c.String(nullable: false, maxLength: 255),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(nullable: false),
                        Wall = c.Single(nullable: false),
                        Dig = c.Single(nullable: false),
                        Brick = c.Single(nullable: false),
                        Remark = c.String(),
                        PlotTypeId = c.Byte(nullable: false),
                        PlotAreaId = c.Int(nullable: false),
                        ApplicantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.PlotAreas", t => t.PlotAreaId)
                .ForeignKey("dbo.PlotTypes", t => t.PlotTypeId)
                .Index(t => t.PlotTypeId)
                .Index(t => t.PlotAreaId)
                .Index(t => t.ApplicantId);
            
            AddColumn("dbo.Deceaseds", "PlotId", c => c.Int());
            CreateIndex("dbo.Deceaseds", "PlotId");
            AddForeignKey("dbo.Deceaseds", "PlotId", "dbo.Plots", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plots", "PlotTypeId", "dbo.PlotTypes");
            DropForeignKey("dbo.Plots", "PlotAreaId", "dbo.PlotAreas");
            DropForeignKey("dbo.Deceaseds", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.Plots", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Plots", new[] { "ApplicantId" });
            DropIndex("dbo.Plots", new[] { "PlotAreaId" });
            DropIndex("dbo.Plots", new[] { "PlotTypeId" });
            DropIndex("dbo.Deceaseds", new[] { "PlotId" });
            DropColumn("dbo.Deceaseds", "PlotId");
            DropTable("dbo.Plots");
        }
    }
}
