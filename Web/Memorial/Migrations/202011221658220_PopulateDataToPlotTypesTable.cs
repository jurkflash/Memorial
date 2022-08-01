namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToPlotTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot) VALUES (1,N'单穴 Single',1,'False')");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot) VALUES (2,N'双穴 Double',2,'False')");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot) VALUES (3,N'新双穴 NewDouble',2,'False')");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot) VALUES (4,N'风水地 FengShui',0,'True')");
        }

        public override void Down()
        {
            Sql("TRUNCATE TABLE PlotTypes");
        }
    }
}
