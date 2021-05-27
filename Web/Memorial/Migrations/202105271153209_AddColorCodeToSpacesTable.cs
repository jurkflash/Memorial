namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColorCodeToSpacesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Spaces", "ColorCode", c => c.String(maxLength: 6));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Spaces", "ColorCode");
        }
    }
}
