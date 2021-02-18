namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateQuadrangleItemsTableData1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE QuadrangleItems SET Name=N'照片 Photo' WHERE SystemCode='Photo'");
            Sql("UPDATE QuadrangleItems SET Name=N'移位 Shift' WHERE SystemCode='Shift'");
        }
        
        public override void Down()
        {
        }
    }
}
