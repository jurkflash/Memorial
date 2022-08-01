namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnNameGenderToNameInGenderTypesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GenderTypes", "Name", c => c.String(nullable: false, maxLength: 255));
            Sql("UPDATE GenderTypes SET Name=Gender");
            DropColumn("dbo.GenderTypes", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GenderTypes", "Gender", c => c.String(nullable: false, maxLength: 255));
            Sql("UPDATE GenderTypes SET Gender=Name");
            DropColumn("dbo.GenderTypes", "Name");
        }
    }
}
