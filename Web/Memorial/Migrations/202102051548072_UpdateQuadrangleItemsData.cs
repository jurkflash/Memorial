namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateQuadrangleItemsData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE QuadrangleItems SET isOrder='True'");
            Sql("UPDATE QuadrangleItems SET isOrder='False' WHERE SystemCode='Free'");
        }
        
        public override void Down()
        {
        }
    }
}
