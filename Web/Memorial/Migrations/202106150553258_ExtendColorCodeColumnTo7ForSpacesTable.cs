namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendColorCodeColumnTo7ForSpacesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Spaces", "ColorCode", c => c.String(maxLength: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Spaces", "ColorCode", c => c.String(maxLength: 6));
        }
    }
}
