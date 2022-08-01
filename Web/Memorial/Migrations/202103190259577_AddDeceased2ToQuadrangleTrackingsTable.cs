namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeceased2ToQuadrangleTrackingsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuadrangleTrackings", "QuadrangleId", "dbo.Quadrangles");
            DropIndex("dbo.QuadrangleTrackings", new[] { "QuadrangleTransactionAF" });
            RenameColumn(table: "dbo.QuadrangleTrackings", name: "DeceasedId", newName: "Deceased1Id");
            RenameIndex(table: "dbo.QuadrangleTrackings", name: "IX_DeceasedId", newName: "IX_Deceased1Id");
            AddColumn("dbo.QuadrangleTrackings", "Deceased2Id", c => c.Int());
            AlterColumn("dbo.QuadrangleTrackings", "QuadrangleTransactionAF", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.QuadrangleTrackings", "QuadrangleTransactionAF");
            CreateIndex("dbo.QuadrangleTrackings", "Deceased2Id");
            AddForeignKey("dbo.QuadrangleTrackings", "Deceased2Id", "dbo.Deceaseds", "Id");
            AddForeignKey("dbo.QuadrangleTrackings", "QuadrangleId", "dbo.Quadrangles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTrackings", "QuadrangleId", "dbo.Quadrangles");
            DropForeignKey("dbo.QuadrangleTrackings", "Deceased2Id", "dbo.Deceaseds");
            DropIndex("dbo.QuadrangleTrackings", new[] { "Deceased2Id" });
            DropIndex("dbo.QuadrangleTrackings", new[] { "QuadrangleTransactionAF" });
            AlterColumn("dbo.QuadrangleTrackings", "QuadrangleTransactionAF", c => c.String(maxLength: 50));
            DropColumn("dbo.QuadrangleTrackings", "Deceased2Id");
            RenameIndex(table: "dbo.QuadrangleTrackings", name: "IX_Deceased1Id", newName: "IX_DeceasedId");
            RenameColumn(table: "dbo.QuadrangleTrackings", name: "Deceased1Id", newName: "DeceasedId");
            CreateIndex("dbo.QuadrangleTrackings", "QuadrangleTransactionAF");
            AddForeignKey("dbo.QuadrangleTrackings", "QuadrangleId", "dbo.Quadrangles", "Id", cascadeDelete: true);
        }
    }
}
