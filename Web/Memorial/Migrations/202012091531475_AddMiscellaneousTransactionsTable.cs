namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMiscellaneousTransactionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MiscellaneousTransactions",
                c => new
                    {
                        AF = c.String(nullable: false, maxLength: 128),
                        BasePrice = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                        Remark = c.String(),
                        MiscellaneousItemId = c.Int(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.MiscellaneousItems", t => t.MiscellaneousItemId)
                .Index(t => t.MiscellaneousItemId)
                .Index(t => t.ApplicantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MiscellaneousTransactions", "MiscellaneousItemId", "dbo.MiscellaneousItems");
            DropForeignKey("dbo.MiscellaneousTransactions", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.MiscellaneousTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.MiscellaneousTransactions", new[] { "MiscellaneousItemId" });
            DropTable("dbo.MiscellaneousTransactions");
        }
    }
}
