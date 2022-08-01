namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAncestorTrackingsToAncestralTabletTrackingsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AncestorTrackings", newName: "AncestralTabletTrackings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AncestralTabletTrackings", newName: "AncestorTrackings");
        }
    }
}
