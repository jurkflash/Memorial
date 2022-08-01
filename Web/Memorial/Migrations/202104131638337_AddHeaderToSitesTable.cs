namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHeaderToSitesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "Header", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "Header");
        }
    }
}
