namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToSpaceItemsTable2 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE SpaceItems SET SystemCode=Name");
        }

        public override void Down()
        {
        }
    }
}
