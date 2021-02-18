namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCodeToPlotTypeAndRepopulateData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotTypes", "Code", c => c.String(nullable: false, maxLength: 10));
            Sql("DELETE PlotTypes");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot,Code) VALUES (1,N'单穴 Single',1,'False','CYSP1')");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot,Code) VALUES (2,N'双穴 Double',2,'False','CYDP2')");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot,Code) VALUES (3,N'新双穴 NewDouble',2,'False','CYDP3')");
            Sql("INSERT INTO PlotTypes(Id,Name,NumberOfPlacement,isFengShuiPlot,Code) VALUES (4,N'风水地 FengShui',0,'True','CYFSD')");
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlotTypes", "Code");
        }
    }
}
