namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePricePerDay_AllowDoubleBook_ToleranceHour_CodeFromSpacesTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Spaces", "PricePerDay");
            DropColumn("dbo.Spaces", "AllowDoubleBook");
            DropColumn("dbo.Spaces", "ToleranceHour");
            DropColumn("dbo.Spaces", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Spaces", "Code", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Spaces", "ToleranceHour", c => c.Byte(nullable: false));
            AddColumn("dbo.Spaces", "AllowDoubleBook", c => c.Boolean(nullable: false));
            AddColumn("dbo.Spaces", "PricePerDay", c => c.Single(nullable: false));
        }
    }
}
