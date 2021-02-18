namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpaceTransactionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpaceTransactions",
                c => new
                    {
                        AF = c.String(nullable: false, maxLength: 128),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        BasePrice = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                        Remark = c.String(),
                        SpaceItemId = c.Int(nullable: false),
                        FuneralCompanyId = c.Int(),
                        ApplicantId = c.Int(nullable: false),
                        DeceasedId = c.Int(),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .ForeignKey("dbo.FuneralCompanies", t => t.FuneralCompanyId)
                .ForeignKey("dbo.SpaceItems", t => t.SpaceItemId)
                .Index(t => t.SpaceItemId)
                .Index(t => t.FuneralCompanyId)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpaceTransactions", "SpaceItemId", "dbo.SpaceItems");
            DropForeignKey("dbo.SpaceTransactions", "FuneralCompanyId", "dbo.FuneralCompanies");
            DropForeignKey("dbo.SpaceTransactions", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.SpaceTransactions", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.SpaceTransactions", new[] { "DeceasedId" });
            DropIndex("dbo.SpaceTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.SpaceTransactions", new[] { "FuneralCompanyId" });
            DropIndex("dbo.SpaceTransactions", new[] { "SpaceItemId" });
            DropTable("dbo.SpaceTransactions");
        }
    }
}
