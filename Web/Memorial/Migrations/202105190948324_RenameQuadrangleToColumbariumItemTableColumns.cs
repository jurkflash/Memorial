namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleToColumbariumItemTableColumns : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuadrangleItems", newName: "ColumbariumItems");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "QuadrangleItemId", newName: "ColumbariumItemId");
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_QuadrangleItemId", newName: "IX_ColumbariumItemId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ColumbariumTransactions", name: "IX_ColumbariumItemId", newName: "IX_QuadrangleItemId");
            RenameColumn(table: "dbo.ColumbariumTransactions", name: "ColumbariumItemId", newName: "QuadrangleItemId");
            RenameTable(name: "dbo.ColumbariumItems", newName: "QuadrangleItems");
        }
    }
}
