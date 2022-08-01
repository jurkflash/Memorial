namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToQuadrangleCentresTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT QuadrangleCentres ON; " +
               "INSERT INTO QuadrangleCentres(Id,Name,SiteId,Code) VALUES (1,N'孝恩亭',2,''); " +
               "SET IDENTITY_INSERT QuadrangleCentres OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleCentres ON; " +
               "INSERT INTO QuadrangleCentres(Id,Name,SiteId,Code) VALUES (2,N'孝思堂',2,''); " +
               "SET IDENTITY_INSERT QuadrangleCentres OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleCentres ON; " +
               "INSERT INTO QuadrangleCentres(Id,Name,SiteId,Code) VALUES (3,N'龙岩宝塔',3,''); " +
               "SET IDENTITY_INSERT QuadrangleCentres OFF;");
        }
        
        public override void Down()
        {
            Sql("TRUNCATE TABLE QuadrangleCentres");
        }
    }
}
