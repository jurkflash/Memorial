namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCremationTransactionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CremationTransactions",
                c => new
                    {
                        AF = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        CremationItemId = c.Int(nullable: false),
                        CremateDate = c.DateTime(nullable: false),
                        FuneralCompanyId = c.Int(),
                        ApplicantId = c.Int(nullable: false),
                        DeceasedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.CremationItems", t => t.CremationItemId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .ForeignKey("dbo.FuneralCompanies", t => t.FuneralCompanyId)
                .Index(t => t.CremationItemId)
                .Index(t => t.FuneralCompanyId)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CremationTransactions", "FuneralCompanyId", "dbo.FuneralCompanies");
            DropForeignKey("dbo.CremationTransactions", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.CremationTransactions", "CremationItemId", "dbo.CremationItems");
            DropForeignKey("dbo.CremationTransactions", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.CremationTransactions", new[] { "DeceasedId" });
            DropIndex("dbo.CremationTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.CremationTransactions", new[] { "FuneralCompanyId" });
            DropIndex("dbo.CremationTransactions", new[] { "CremationItemId" });
            DropTable("dbo.CremationTransactions");
        }
    }
}
