namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAncestorTransactionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AncestorTransactions",
                c => new
                    {
                        AF = c.String(nullable: false, maxLength: 50),
                        AncestorItemId = c.Int(nullable: false),
                        AncestorId = c.Int(nullable: false),
                        Label = c.String(),
                        Remark = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                        DeceasedId = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Ancestors", t => t.AncestorId, cascadeDelete: true)
                .ForeignKey("dbo.AncestorItems", t => t.AncestorItemId)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .Index(t => t.AncestorItemId)
                .Index(t => t.AncestorId)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AncestorTransactions", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.AncestorTransactions", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.AncestorTransactions", "AncestorItemId", "dbo.AncestorItems");
            DropForeignKey("dbo.AncestorTransactions", "AncestorId", "dbo.Ancestors");
            DropIndex("dbo.AncestorTransactions", new[] { "DeceasedId" });
            DropIndex("dbo.AncestorTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.AncestorTransactions", new[] { "AncestorId" });
            DropIndex("dbo.AncestorTransactions", new[] { "AncestorItemId" });
            DropTable("dbo.AncestorTransactions");
        }
    }
}
