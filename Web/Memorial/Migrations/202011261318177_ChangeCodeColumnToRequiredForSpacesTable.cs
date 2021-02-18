namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCodeColumnToRequiredForSpacesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Spaces", "Code", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Spaces", "Code");
        }
    }
}
