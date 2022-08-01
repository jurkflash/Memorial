namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleToNicheTableColumns : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Quadrangles", newName: "Niches");
            RenameColumn(table: "dbo.ColumbariumTrackings", name: "QuadrangleId", newName: "NicheId");
            RenameColumn(table: "dbo.ColumbariumTrackings", name: "Quadrangle_Id", newName: "Niche_Id");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "QuadrangleId", newName: "NicheId");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "ShiftedQuadrangleId", newName: "ShiftedNicheId");
            RenameColumn(table: "dbo.Deceaseds", name: "QuadrangleId", newName: "NicheId");
            RenameColumn(table: "dbo.Niches", name: "QuadrangleAreaId", newName: "ColumbariumAreaId");
            RenameIndex(table: "dbo.Deceaseds", name: "IX_QuadrangleId", newName: "IX_NicheId");
            RenameIndex(table: "dbo.ColumbariumTrackings", name: "IX_QuadrangleId", newName: "IX_NicheId");
            RenameIndex(table: "dbo.ColumbariumTrackings", name: "IX_Quadrangle_Id", newName: "IX_Niche_Id");
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_QuadrangleId", newName: "IX_NicheId");
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_ShiftedQuadrangleId", newName: "IX_ShiftedNicheId");
            RenameIndex(table: "dbo.Niches", name: "IX_QuadrangleAreaId", newName: "IX_ColumbariumAreaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Niches", name: "IX_ColumbariumAreaId", newName: "IX_QuadrangleAreaId");
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_ShiftedNicheId", newName: "IX_ShiftedQuadrangleId");
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_NicheId", newName: "IX_QuadrangleId");
            RenameIndex(table: "dbo.ColumbariumTrackings", name: "IX_Niche_Id", newName: "IX_Quadrangle_Id");
            RenameIndex(table: "dbo.ColumbariumTrackings", name: "IX_NicheId", newName: "IX_QuadrangleId");
            RenameIndex(table: "dbo.Deceaseds", name: "IX_NicheId", newName: "IX_QuadrangleId");
            RenameColumn(table: "dbo.Niches", name: "ColumbariumAreaId", newName: "QuadrangleAreaId");
            RenameColumn(table: "dbo.Deceaseds", name: "NicheId", newName: "QuadrangleId");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "ShiftedNicheId", newName: "ShiftedQuadrangleId");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "NicheId", newName: "QuadrangleId");
            RenameColumn(table: "dbo.ColumbariumTrackings", name: "Niche_Id", newName: "Quadrangle_Id");
            RenameColumn(table: "dbo.ColumbariumTrackings", name: "NicheId", newName: "QuadrangleId");
            RenameTable(name: "dbo.Niches", newName: "Quadrangles");
        }
    }
}
