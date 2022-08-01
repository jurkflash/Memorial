namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuadrangleToColumbariumNumberTableColumns : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuadrangleNumbers", newName: "ColumbariumNumbers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ColumbariumNumbers", newName: "QuadrangleNumbers");
        }
    }
}
