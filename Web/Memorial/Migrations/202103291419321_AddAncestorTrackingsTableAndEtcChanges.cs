namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAncestorTrackingsTableAndEtcChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AncestorTrackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AncestorId = c.Int(nullable: false),
                        AncestorTransactionAF = c.String(nullable: false, maxLength: 50),
                        ApplicantId = c.Int(),
                        DeceasedId = c.Int(),
                        ActionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ancestors", t => t.AncestorId)
                .ForeignKey("dbo.AncestorTransactions", t => t.AncestorTransactionAF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .Index(t => t.AncestorId)
                .Index(t => t.AncestorTransactionAF)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId);
            
            AddColumn("dbo.Deceaseds", "AncestorId", c => c.Int());
            CreateIndex("dbo.Deceaseds", "AncestorId");
            AddForeignKey("dbo.Deceaseds", "AncestorId", "dbo.Ancestors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deceaseds", "AncestorId", "dbo.Ancestors");
            DropForeignKey("dbo.AncestorTrackings", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.AncestorTrackings", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.AncestorTrackings", "AncestorTransactionAF", "dbo.AncestorTransactions");
            DropForeignKey("dbo.AncestorTrackings", "AncestorId", "dbo.Ancestors");
            DropIndex("dbo.Deceaseds", new[] { "AncestorId" });
            DropIndex("dbo.AncestorTrackings", new[] { "DeceasedId" });
            DropIndex("dbo.AncestorTrackings", new[] { "ApplicantId" });
            DropIndex("dbo.AncestorTrackings", new[] { "AncestorTransactionAF" });
            DropIndex("dbo.AncestorTrackings", new[] { "AncestorId" });
            DropColumn("dbo.Deceaseds", "AncestorId");
            DropTable("dbo.AncestorTrackings");
        }
    }
}
