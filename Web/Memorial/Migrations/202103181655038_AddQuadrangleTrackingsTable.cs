namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuadrangleTrackingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuadrangleTrackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuadrangleId = c.Int(nullable: false),
                        QuadrangleTransactionAF = c.String(maxLength: 50),
                        ApplicantId = c.Int(),
                        Deceased1Id = c.Int(),
                        ActionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased1Id)
                .ForeignKey("dbo.Quadrangles", t => t.QuadrangleId, cascadeDelete: true)
                .ForeignKey("dbo.QuadrangleTransactions", t => t.QuadrangleTransactionAF)
                .Index(t => t.QuadrangleId)
                .Index(t => t.QuadrangleTransactionAF)
                .Index(t => t.ApplicantId)
                .Index(t => t.Deceased1Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTrackings", "QuadrangleTransactionAF", "dbo.QuadrangleTransactions");
            DropForeignKey("dbo.QuadrangleTrackings", "QuadrangleId", "dbo.Quadrangles");
            DropForeignKey("dbo.QuadrangleTrackings", "Deceased1Id", "dbo.Deceaseds");
            DropForeignKey("dbo.QuadrangleTrackings", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.QuadrangleTrackings", new[] { "Deceased1Id" });
            DropIndex("dbo.QuadrangleTrackings", new[] { "ApplicantId" });
            DropIndex("dbo.QuadrangleTrackings", new[] { "QuadrangleTransactionAF" });
            DropIndex("dbo.QuadrangleTrackings", new[] { "QuadrangleId" });
            DropTable("dbo.QuadrangleTrackings");
        }
    }
}
