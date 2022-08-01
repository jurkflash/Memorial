namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMiscellaneousItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MiscellaneousItems SET SystemCode='Donations' WHERE Id=1");
        }
        
        public override void Down()
        {
        }
    }
}
