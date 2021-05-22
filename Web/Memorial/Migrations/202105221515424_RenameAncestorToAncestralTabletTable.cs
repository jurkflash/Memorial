namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAncestorToAncestralTabletTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Ancestors", newName: "AncestralTablets");
            RenameColumn(table: "dbo.AncestralTabletTrackings", name: "AncestorId", newName: "AncestralTabletId");
            RenameColumn(table: "dbo.AncestralTabletTransactions", name: "AncestorId", newName: "AncestralTabletId");
            RenameColumn(table: "dbo.Deceaseds", name: "AncestorId", newName: "AncestralTabletId");
            RenameColumn(table: "dbo.AncestralTabletTransactions", name: "ShiftedAncestorId", newName: "ShiftedAncestralTabletId");
            RenameIndex(table: "dbo.AncestralTabletTransactions", name: "IX_AncestorId", newName: "IX_AncestralTabletId");
            RenameIndex(table: "dbo.AncestralTabletTransactions", name: "IX_ShiftedAncestorId", newName: "IX_ShiftedAncestralTabletId");
            RenameIndex(table: "dbo.AncestralTabletTrackings", name: "IX_AncestorId", newName: "IX_AncestralTabletId");
            RenameIndex(table: "dbo.Deceaseds", name: "IX_AncestorId", newName: "IX_AncestralTabletId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Deceaseds", name: "IX_AncestralTabletId", newName: "IX_AncestorId");
            RenameIndex(table: "dbo.AncestralTabletTrackings", name: "IX_AncestralTabletId", newName: "IX_AncestorId");
            RenameIndex(table: "dbo.AncestralTabletTransactions", name: "IX_ShiftedAncestralTabletId", newName: "IX_ShiftedAncestorId");
            RenameIndex(table: "dbo.AncestralTabletTransactions", name: "IX_AncestralTabletId", newName: "IX_AncestorId");
            RenameColumn(table: "dbo.AncestralTabletTransactions", name: "ShiftedAncestralTabletId", newName: "ShiftedAncestorId");
            RenameColumn(table: "dbo.Deceaseds", name: "AncestralTabletId", newName: "AncestorId");
            RenameColumn(table: "dbo.AncestralTabletTransactions", name: "AncestralTabletId", newName: "AncestorId");
            RenameColumn(table: "dbo.AncestralTabletTrackings", name: "AncestralTabletId", newName: "AncestorId");
            RenameTable(name: "dbo.AncestralTablets", newName: "Ancestors");
        }
    }
}
