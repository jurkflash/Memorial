namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSpaceItemsTable2 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE SpaceItems SET Name=N'預訂 Booking' WHERE Name like '%Booking'");
            Sql("UPDATE SpaceItems SET Name=N'免費 Free' WHERE Name like '%Free'");
            Sql("UPDATE SpaceItems SET Name=N'租椅子 Chair' WHERE Name like '%Chair'");
            Sql("UPDATE SpaceItems SET Name=N'電費 Electric' WHERE Name like '%Electric'");
            Sql("UPDATE SpaceItems SET Name=N'提早放靈屋 House' WHERE Name like '%House'");
        }
        
        public override void Down()
        {
        }
    }
}
