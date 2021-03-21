namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameDeceased1ToDeceasedForQuadrangleTrackingsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.QuadrangleTrackings", name: "Deceased1Id", newName: "DeceasedId");
            RenameIndex(table: "dbo.QuadrangleTrackings", name: "IX_Deceased1Id", newName: "IX_DeceasedId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.QuadrangleTrackings", name: "IX_DeceasedId", newName: "IX_Deceased1Id");
            RenameColumn(table: "dbo.QuadrangleTrackings", name: "DeceasedId", newName: "Deceased1Id");
        }
    }
}
