namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLabelColumnFromAncestorsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ancestors", "Label");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ancestors", "Label", c => c.String());
        }
    }
}
