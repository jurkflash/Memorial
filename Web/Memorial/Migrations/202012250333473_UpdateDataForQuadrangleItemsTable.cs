namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataForQuadrangleItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE QuadrangleItems SET SystemCode=Name");
            Sql("UPDATE QuadrangleItems SET Name=N'單 Order',SystemCode='Order' WHERE Name='Quadrangle'");
            Sql("UPDATE QuadrangleItems SET Name=N'管理 Manage',SystemCode='Manage' WHERE Name='Maintenance'");
            Sql("UPDATE SpaceItems SET Name=N'照片 Free' WHERE Name = 'Photo'");
            Sql("UPDATE SpaceItems SET Name=N'移位 Chair' WHERE Name = 'Shift'");
            Sql("UPDATE SpaceItems SET Name=N'轉讓 Transfer' WHERE Name = 'Transfer'");
            Sql("UPDATE SpaceItems SET Name=N'免費 Free' WHERE Name = 'Free'");

        }

        public override void Down()
        {
        }
    }
}
