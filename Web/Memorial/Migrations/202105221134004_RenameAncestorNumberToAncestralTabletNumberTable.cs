namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAncestorNumberToAncestralTabletNumberTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AncestorNumbers", newName: "AncestralTabletNumbers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AncestralTabletNumbers", newName: "AncestorNumbers");
        }
    }
}
