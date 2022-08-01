namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleToColumbariumAreaTableColumns : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuadrangleAreas", newName: "ColumbariumAreas");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ColumbariumAreas", newName: "QuadrangleAreas");
        }
    }
}
