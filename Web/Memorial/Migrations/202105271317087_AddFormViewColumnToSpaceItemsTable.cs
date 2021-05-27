namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFormViewColumnToSpaceItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpaceItems", "FormView", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpaceItems", "FormView");
        }
    }
}
