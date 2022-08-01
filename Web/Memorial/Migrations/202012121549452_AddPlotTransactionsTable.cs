namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlotTransactionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlotTransactions",
                c => new
                    {
                        AF = c.String(nullable: false, maxLength: 50),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(nullable: false),
                        Wall = c.Single(nullable: false),
                        Dig = c.Single(nullable: false),
                        Brick = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        PlotId = c.Int(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Plots", t => t.PlotId)
                .Index(t => t.PlotId)
                .Index(t => t.ApplicantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotTransactions", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.PlotTransactions", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.PlotTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.PlotTransactions", new[] { "PlotId" });
            DropTable("dbo.PlotTransactions");
        }
    }
}
