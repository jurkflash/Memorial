namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasFreeOrderColumnToQuadranglesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quadrangles", "hasFreeOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quadrangles", "hasFreeOrder");
        }
    }
}
