namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasFreeOrderToAncestorsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ancestors", "hasFreeOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ancestors", "hasFreeOrder");
        }
    }
}
