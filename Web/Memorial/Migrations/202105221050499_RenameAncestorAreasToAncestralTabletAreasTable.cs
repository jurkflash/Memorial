namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAncestorAreasToAncestralTabletAreasTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AncestorAreas", newName: "AncestralTabletAreas");
            RenameColumn(table: "dbo.AncestorItems", name: "AncestorAreaId", newName: "AncestralTabletAreaId");
            RenameColumn(table: "dbo.Ancestors", name: "AncestorAreaId", newName: "AncestralTabletAreaId");
            RenameIndex(table: "dbo.AncestorItems", name: "IX_AncestorAreaId", newName: "IX_AncestralTabletAreaId");
            RenameIndex(table: "dbo.Ancestors", name: "IX_AncestorAreaId", newName: "IX_AncestralTabletAreaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Ancestors", name: "IX_AncestralTabletAreaId", newName: "IX_AncestorAreaId");
            RenameIndex(table: "dbo.AncestorItems", name: "IX_AncestralTabletAreaId", newName: "IX_AncestorAreaId");
            RenameColumn(table: "dbo.Ancestors", name: "AncestralTabletAreaId", newName: "AncestorAreaId");
            RenameColumn(table: "dbo.AncestorItems", name: "AncestralTabletAreaId", newName: "AncestorAreaId");
            RenameTable(name: "dbo.AncestralTabletAreas", newName: "AncestorAreas");
        }
    }
}
