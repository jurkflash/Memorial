namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnCodeFromCremationsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cremations", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cremations", "Code", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
