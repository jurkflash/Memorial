namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumnNameMaritalToNameInMaritalTypesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaritalTypes", "Name", c => c.String(nullable: false, maxLength: 255));
            Sql("UPDATE MaritalTypes SET Name=Marital");
            DropColumn("dbo.MaritalTypes", "Marital");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MaritalTypes", "Marital", c => c.String(nullable: false, maxLength: 255));
            Sql("UPDATE MaritalTypes SET Marital=Name");
            DropColumn("dbo.MaritalTypes", "Name");
        }
    }
}
