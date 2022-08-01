namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenderTypeConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GenderTypes", "Gender", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GenderTypes", "Gender", c => c.String());
        }
    }
}
