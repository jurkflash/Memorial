namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleToColumbariumTrackingTableColumns : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuadrangleTrackings", newName: "ColumbariumTrackings");
            RenameColumn(table: "dbo.ColumbariumTrackings", name: "QuadrangleTransactionAF", newName: "ColumbariumTransactionAF");
            RenameIndex(table: "dbo.ColumbariumTrackings", name: "IX_QuadrangleTransactionAF", newName: "IX_ColumbariumTransactionAF");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ColumbariumTrackings", name: "IX_ColumbariumTransactionAF", newName: "IX_QuadrangleTransactionAF");
            RenameColumn(table: "dbo.ColumbariumTrackings", name: "ColumbariumTransactionAF", newName: "QuadrangleTransactionAF");
            RenameTable(name: "dbo.ColumbariumTrackings", newName: "QuadrangleTrackings");
        }
    }
}
