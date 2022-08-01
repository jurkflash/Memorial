namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataForQuadrangleItemsTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE QuadrangleItems SET Name=N'照片 Free' WHERE Name = 'Photo'");
            Sql("UPDATE QuadrangleItems SET Name=N'移位 Chair' WHERE Name = 'Shift'");
            Sql("UPDATE QuadrangleItems SET Name=N'轉讓 Transfer' WHERE Name = 'Transfer'");
            Sql("UPDATE QuadrangleItems SET Name=N'免費 Free' WHERE Name = 'Free'");
        }
        
        public override void Down()
        {
        }
    }
}
