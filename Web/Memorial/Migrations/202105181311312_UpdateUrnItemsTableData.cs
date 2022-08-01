namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUrnItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE UrnItems SET SystemCode='Purchases' WHERE SystemCode='Purchase'");
        }
        
        public override void Down()
        {
        }
    }
}
