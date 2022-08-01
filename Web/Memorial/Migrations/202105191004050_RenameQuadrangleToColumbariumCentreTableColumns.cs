namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleToColumbariumCentreTableColumns : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuadrangleCentres", newName: "ColumbariumCentres");
            RenameColumn(table: "dbo.QuadrangleAreas", name: "QuadrangleCentreId", newName: "ColumbariumCentreId");
            RenameIndex(table: "dbo.QuadrangleAreas", name: "IX_QuadrangleCentreId", newName: "IX_ColumbariumCentreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.QuadrangleAreas", name: "IX_ColumbariumCentreId", newName: "IX_QuadrangleCentreId");
            RenameColumn(table: "dbo.QuadrangleAreas", name: "ColumbariumCentreId", newName: "QuadrangleCentreId");
            RenameTable(name: "dbo.ColumbariumCentres", newName: "QuadrangleCentres");
        }
    }
}
