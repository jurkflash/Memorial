namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToPlotTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot,ActiveStatus,CreatedUtcTime) VALUES (1,'单穴 Single',1,0,1,GETUTCDATE()); ");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot,ActiveStatus,CreatedUtcTime) VALUES (2,'双穴 Double',2,0,1,GETUTCDATE()); ");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot,ActiveStatus,CreatedUtcTime) VALUES (3,'新双穴 NewDouble',2,0,1,GETUTCDATE()); ");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot,ActiveStatus,CreatedUtcTime) VALUES (4,'风水地 FengShui',0,1,1,GETUTCDATE()); ");
        }

        public override void Down()
        {
        }
    }
}
