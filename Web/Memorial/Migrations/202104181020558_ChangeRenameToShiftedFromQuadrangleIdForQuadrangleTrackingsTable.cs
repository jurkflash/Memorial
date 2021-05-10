namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRenameToShiftedFromQuadrangleIdForQuadrangleTrackingsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.QuadrangleTrackings", name: "ShiftedQuadrangleId", newName: "ShiftedFromQuadrangleId");
            RenameIndex(table: "dbo.QuadrangleTrackings", name: "IX_ShiftedQuadrangleId", newName: "IX_ShiftedFromQuadrangleId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.QuadrangleTrackings", name: "IX_ShiftedFromQuadrangleId", newName: "IX_ShiftedQuadrangleId");
            RenameColumn(table: "dbo.QuadrangleTrackings", name: "ShiftedFromQuadrangleId", newName: "ShiftedQuadrangleId");
        }
    }
}
