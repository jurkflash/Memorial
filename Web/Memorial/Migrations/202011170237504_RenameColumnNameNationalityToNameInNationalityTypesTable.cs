namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumnNameNationalityToNameInNationalityTypesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NationalityTypes", "Name", c => c.String(nullable: false, maxLength: 255));
            Sql("UPDATE NationalityTypes SET Name=Nationality");
            DropColumn("dbo.NationalityTypes", "Nationality");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NationalityTypes", "Nationality", c => c.String(nullable: false, maxLength: 255));
            Sql("UPDATE NationalityTypes SET Nationality=Name");
            DropColumn("dbo.NationalityTypes", "Name");
        }
    }
}
