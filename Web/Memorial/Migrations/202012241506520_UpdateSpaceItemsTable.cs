namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSpaceItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE SpaceItems SET Name=N'預訂 Booking' WHERE Name='Booking'");
            Sql("UPDATE SpaceItems SET Name=N'免費 Free' WHERE Name='Free'");
            Sql("UPDATE SpaceItems SET Name=N'租椅子 Chair' WHERE Name='Chair'");
            Sql("UPDATE SpaceItems SET Name=N'電費 Electric' WHERE Name='Electric'");
            Sql("UPDATE SpaceItems SET Name=N'提早放靈屋 House' WHERE Name='House'");
        }
        
        public override void Down()
        {
        }
    }
}
