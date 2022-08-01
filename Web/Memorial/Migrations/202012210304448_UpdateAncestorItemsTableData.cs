namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAncestorItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE AncestorItems SET SystemCode=Name");
        }
        
        public override void Down()
        {
        }
    }
}
