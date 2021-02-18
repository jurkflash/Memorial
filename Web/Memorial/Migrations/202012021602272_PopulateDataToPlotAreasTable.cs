namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToPlotAreasTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT PlotAreas ON; " +
               "INSERT INTO PlotAreas(Id,Name,SiteId) VALUES (1,N'极乐山庄',1); " +
               "SET IDENTITY_INSERT PlotAreas OFF;");
            Sql("SET IDENTITY_INSERT PlotAreas ON; " +
               "INSERT INTO PlotAreas(Id,Name,SiteId) VALUES (2,N'帝沙再也新山',3); " +
               "SET IDENTITY_INSERT PlotAreas OFF;");
            Sql("SET IDENTITY_INSERT PlotAreas ON; " +
               "INSERT INTO PlotAreas(Id,Name,SiteId) VALUES (3,N'帝沙再也旧山',3); " +
               "SET IDENTITY_INSERT PlotAreas OFF;");
            Sql("SET IDENTITY_INSERT PlotAreas ON; " +
               "INSERT INTO PlotAreas(Id,Name,SiteId) VALUES (4,N'帝沙再也新山风水地',3); " +
               "SET IDENTITY_INSERT PlotAreas OFF;");
            Sql("SET IDENTITY_INSERT PlotAreas ON; " +
               "INSERT INTO PlotAreas(Id,Name,SiteId) VALUES (5,N'极乐山庄风水地',1); " +
               "SET IDENTITY_INSERT PlotAreas OFF;");

        }

        public override void Down()
        {
            Sql("TRUNCATE TABLE PlotAreas");
        }
    }
}
