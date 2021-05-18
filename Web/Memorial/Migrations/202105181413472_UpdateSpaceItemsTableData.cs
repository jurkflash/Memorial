namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSpaceItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE SpaceItems SET SystemCode='Bookings' WHERE SystemCode='Booking'");
            Sql("UPDATE SpaceItems SET SystemCode='Chairs' WHERE SystemCode='Chair'");
            Sql("UPDATE SpaceItems SET SystemCode='Houses' WHERE SystemCode='House'");
        }
        
        public override void Down()
        {
        }
    }
}
