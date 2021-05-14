namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveShiftedFromQuadrangleFromQuadrangleTrackingsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.QuadrangleTrackings", name: "ShiftedFromQuadrangleId", newName: "Quadrangle_Id");
            RenameIndex(table: "dbo.QuadrangleTrackings", name: "IX_ShiftedFromQuadrangleId", newName: "IX_Quadrangle_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.QuadrangleTrackings", name: "IX_Quadrangle_Id", newName: "IX_ShiftedFromQuadrangleId");
            RenameColumn(table: "dbo.QuadrangleTrackings", name: "Quadrangle_Id", newName: "ShiftedFromQuadrangleId");
        }
    }
}
