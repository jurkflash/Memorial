namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleTypeToNicheTypeTableColumns : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuadrangleTypes", newName: "NicheTypes");
            RenameColumn(table: "dbo.Niches", name: "QuadrangleTypeId", newName: "NicheTypeId");
            RenameIndex(table: "dbo.Niches", name: "IX_QuadrangleTypeId", newName: "IX_NicheTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Niches", name: "IX_NicheTypeId", newName: "IX_QuadrangleTypeId");
            RenameColumn(table: "dbo.Niches", name: "NicheTypeId", newName: "QuadrangleTypeId");
            RenameTable(name: "dbo.NicheTypes", newName: "QuadrangleTypes");
        }
    }
}
