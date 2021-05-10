namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhasClearedColumnToPlotsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plots", "hasCleared", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plots", "hasCleared");
        }
    }
}
