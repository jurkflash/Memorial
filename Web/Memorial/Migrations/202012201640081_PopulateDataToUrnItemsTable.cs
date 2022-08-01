namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToUrnItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE UrnItems SET SystemCode=Name");
        }
        
        public override void Down()
        {
        }
    }
}
