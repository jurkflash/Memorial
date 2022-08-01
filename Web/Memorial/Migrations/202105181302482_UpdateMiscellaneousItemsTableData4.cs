namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMiscellaneousItemsTableData4 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MiscellaneousItems SET SystemCode='Compensates' WHERE Id=2");
            Sql("UPDATE MiscellaneousItems SET SystemCode='RentAirCoolers' WHERE Id=3");
            Sql("UPDATE MiscellaneousItems SET SystemCode='Reciprocates' WHERE Id=4");
        }
        
        public override void Down()
        {
        }
    }
}
