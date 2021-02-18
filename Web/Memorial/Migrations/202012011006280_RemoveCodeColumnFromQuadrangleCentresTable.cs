namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCodeColumnFromQuadrangleCentresTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.QuadrangleCentres", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuadrangleCentres", "Code", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
