namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnCodeFromUrnsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Urns", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Urns", "Code", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
