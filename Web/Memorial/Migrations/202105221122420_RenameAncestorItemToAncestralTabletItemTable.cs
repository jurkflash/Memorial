namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAncestorItemToAncestralTabletItemTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AncestorItems", newName: "AncestralTabletItems");
            RenameColumn(table: "dbo.AncestralTabletTransactions", name: "AncestorItemId", newName: "AncestralTabletItemId");
            RenameIndex(table: "dbo.AncestralTabletTransactions", name: "IX_AncestorItemId", newName: "IX_AncestralTabletItemId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AncestralTabletTransactions", name: "IX_AncestralTabletItemId", newName: "IX_AncestorItemId");
            RenameColumn(table: "dbo.AncestralTabletTransactions", name: "AncestralTabletItemId", newName: "AncestorItemId");
            RenameTable(name: "dbo.AncestralTabletItems", newName: "AncestorItems");
        }
    }
}
