namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlotAreasToCemeteryAreasTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlotAreas", newName: "CemeteryAreas");
            RenameColumn(table: "dbo.Plots", name: "PlotAreaId", newName: "CemeteryAreaId");
            RenameIndex(table: "dbo.Plots", name: "IX_PlotAreaId", newName: "IX_CemeteryAreaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Plots", name: "IX_CemeteryAreaId", newName: "IX_PlotAreaId");
            RenameColumn(table: "dbo.Plots", name: "CemeteryAreaId", newName: "PlotAreaId");
            RenameTable(name: "dbo.CemeteryAreas", newName: "PlotAreas");
        }
    }
}
