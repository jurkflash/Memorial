namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlotTypesTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE PlotTypes SET Name=N'单穴 Single' WHERE Id = 1");
            Sql("UPDATE PlotTypes SET Name=N'双穴 Double' WHERE Id = 2");
            Sql("UPDATE PlotTypes SET Name=N'新双穴 NewDouble' WHERE Id = 3");
            Sql("UPDATE PlotTypes SET Name=N'风水地 FengShui' WHERE Id = 4");
        }

        public override void Down()
        {
        }
    }
}
