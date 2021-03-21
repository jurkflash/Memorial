namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameDeceasedToDeceased1ForQuadrangleTransaction : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.QuadrangleTransactions", name: "DeceasedId", newName: "Deceased1Id");
            RenameIndex(table: "dbo.QuadrangleTransactions", name: "IX_DeceasedId", newName: "IX_Deceased1Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.QuadrangleTransactions", name: "IX_Deceased1Id", newName: "IX_DeceasedId");
            RenameColumn(table: "dbo.QuadrangleTransactions", name: "Deceased1Id", newName: "DeceasedId");
        }
    }
}
