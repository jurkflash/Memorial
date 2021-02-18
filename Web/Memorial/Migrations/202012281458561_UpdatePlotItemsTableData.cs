namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlotItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE PlotItems SET SystemCode='PlotSingle' WHERE Name=N'墓地-單穴'");
            Sql("UPDATE PlotItems SET SystemCode='PlotDouble' WHERE Name=N'墓地-雙穴'");
            Sql("UPDATE PlotItems SET SystemCode='PlotNewDouble' WHERE Name=N'墓地-新雙穴'");
            Sql("UPDATE PlotItems SET SystemCode='PlotFengShui' WHERE Name=N'墓地-風水地'");
            Sql("UPDATE PlotItems SET SystemCode='PlotFengShuiTransfer' WHERE Name=N'風水地-轉讓費'");
        }

        public override void Down()
        {
        }
    }
}
