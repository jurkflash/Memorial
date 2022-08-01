namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhasDeceasedColumnToQuadranglesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quadrangles", "hasDeceased", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quadrangles", "hasDeceased");
        }
    }
}
