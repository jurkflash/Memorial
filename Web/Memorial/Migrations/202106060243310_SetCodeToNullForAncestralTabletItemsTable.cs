namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetCodeToNullForAncestralTabletItemsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AncestralTabletItems", "Code", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AncestralTabletItems", "Code", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
