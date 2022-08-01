namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuadrangleTransactionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuadrangleTransactions",
                c => new
                    {
                        AF = c.String(nullable: false, maxLength: 255),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(nullable: false),
                        LifeTimeMaintenance = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        QuadrangleId = c.Int(nullable: false),
                        FuneralCompanyId = c.Int(),
                        ApplicantId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.FuneralCompanies", t => t.FuneralCompanyId)
                .ForeignKey("dbo.Quadrangles", t => t.QuadrangleId)
                .Index(t => t.QuadrangleId)
                .Index(t => t.FuneralCompanyId)
                .Index(t => t.ApplicantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTransactions", "QuadrangleId", "dbo.Quadrangles");
            DropForeignKey("dbo.QuadrangleTransactions", "FuneralCompanyId", "dbo.FuneralCompanies");
            DropForeignKey("dbo.QuadrangleTransactions", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.QuadrangleTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.QuadrangleTransactions", new[] { "FuneralCompanyId" });
            DropIndex("dbo.QuadrangleTransactions", new[] { "QuadrangleId" });
            DropTable("dbo.QuadrangleTransactions");
        }
    }
}
