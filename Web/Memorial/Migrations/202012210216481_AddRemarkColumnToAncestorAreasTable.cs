namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRemarkColumnToAncestorAreasTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestorAreas", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AncestorAreas", "Remark");
        }
    }
}
