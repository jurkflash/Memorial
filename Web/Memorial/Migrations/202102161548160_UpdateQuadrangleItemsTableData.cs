namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateQuadrangleItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE QuadrangleItems SET Price=150 WHERE SystemCode='Manage'");
            Sql("UPDATE QuadrangleItems SET Price=150 WHERE SystemCode='Shift'");
            Sql("UPDATE QuadrangleItems SET Name='照片 Photo' WHERE SystemCode='Photo'");
            Sql("UPDATE QuadrangleItems SET Name='移位 Shift' WHERE SystemCode='Shift'");
        }
        
        public override void Down()
        {
        }
    }
}
