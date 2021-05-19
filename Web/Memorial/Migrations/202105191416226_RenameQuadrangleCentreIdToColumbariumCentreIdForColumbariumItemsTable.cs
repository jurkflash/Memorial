namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleCentreIdToColumbariumCentreIdForColumbariumItemsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ColumbariumItems", name: "QuadrangleCentreId", newName: "ColumbariumCentreId");
            RenameIndex(table: "dbo.ColumbariumItems", name: "IX_QuadrangleCentreId", newName: "IX_ColumbariumCentreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ColumbariumItems", name: "IX_ColumbariumCentreId", newName: "IX_QuadrangleCentreId");
            RenameColumn(table: "dbo.ColumbariumItems", name: "ColumbariumCentreId", newName: "QuadrangleCentreId");
        }
    }
}
