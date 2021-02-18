namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameLocationColumnToPositionForAncestorsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ancestors", "PositionX", c => c.Byte(nullable: false));
            AddColumn("dbo.Ancestors", "PositionY", c => c.Byte(nullable: false));
            DropColumn("dbo.Ancestors", "LocationX");
            DropColumn("dbo.Ancestors", "LocationY");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ancestors", "LocationY", c => c.Byte(nullable: false));
            AddColumn("dbo.Ancestors", "LocationX", c => c.Byte(nullable: false));
            DropColumn("dbo.Ancestors", "PositionY");
            DropColumn("dbo.Ancestors", "PositionX");
        }
    }
}
