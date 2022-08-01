namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemCodeColumnToPlotItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotItems", "SystemCode", c => c.String());

            Sql("UPDATE PlotItems SET SystemCode='PlotSingle' WHERE Name=N'墓地 - 單穴'");
            Sql("UPDATE PlotItems SET SystemCode='PlotDouble' WHERE Name=N'墓地 - 雙穴'");
            Sql("UPDATE PlotItems SET SystemCode='PlotNewDouble' WHERE Name=N'墓地 - 新雙穴'");
            Sql("UPDATE PlotItems SET SystemCode='PlotFengShui' WHERE Name=N'墓地 - 風水地'");
            Sql("UPDATE PlotItems SET SystemCode='PlotFengShuiTransfer' WHERE Name=N'風水地 - 轉讓費'");
            Sql("UPDATE PlotItems SET SystemCode='SecondBurial' WHERE Name=N'附葬'");
            Sql("UPDATE PlotItems SET SystemCode='Clearance' WHERE Name=N'拾金'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlotItems", "SystemCode");
        }
    }
}
