namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotTrackingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlotTrackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlotId = c.Int(nullable: false),
                        PlotTransactionAF = c.String(nullable: false, maxLength: 50),
                        ApplicantId = c.Int(),
                        Deceased1Id = c.Int(),
                        Deceased2Id = c.Int(),
                        ActionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased1Id)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased2Id)
                .ForeignKey("dbo.Plots", t => t.PlotId)
                .ForeignKey("dbo.PlotTransactions", t => t.PlotTransactionAF)
                .Index(t => t.PlotId)
                .Index(t => t.PlotTransactionAF)
                .Index(t => t.ApplicantId)
                .Index(t => t.Deceased1Id)
                .Index(t => t.Deceased2Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotTrackings", "PlotTransactionAF", "dbo.PlotTransactions");
            DropForeignKey("dbo.PlotTrackings", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.PlotTrackings", "Deceased2Id", "dbo.Deceaseds");
            DropForeignKey("dbo.PlotTrackings", "Deceased1Id", "dbo.Deceaseds");
            DropForeignKey("dbo.PlotTrackings", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.PlotTrackings", new[] { "Deceased2Id" });
            DropIndex("dbo.PlotTrackings", new[] { "Deceased1Id" });
            DropIndex("dbo.PlotTrackings", new[] { "ApplicantId" });
            DropIndex("dbo.PlotTrackings", new[] { "PlotTransactionAF" });
            DropIndex("dbo.PlotTrackings", new[] { "PlotId" });
            DropTable("dbo.PlotTrackings");
        }
    }
}
