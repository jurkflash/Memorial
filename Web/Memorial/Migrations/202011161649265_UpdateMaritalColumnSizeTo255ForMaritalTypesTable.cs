namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMaritalColumnSizeTo255ForMaritalTypesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MaritalTypes", "Marital", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaritalTypes", "Marital", c => c.String(nullable: false));
        }
    }
}
