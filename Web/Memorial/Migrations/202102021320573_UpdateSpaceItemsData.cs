namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSpaceItemsData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE SpaceItems SET isOrder='True' WHERE Name<>N'免費 Free'");
        }
        
        public override void Down()
        {
        }
    }
}
