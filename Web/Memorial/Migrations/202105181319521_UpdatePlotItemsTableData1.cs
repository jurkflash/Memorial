namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlotItemsTableData1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE PlotItems SET SystemCode='Orders' WHERE SystemCode='Order'");
            Sql("UPDATE PlotItems SET SystemCode='Clearances' WHERE SystemCode='Clearance'");
            Sql("UPDATE PlotItems SET SystemCode='FengShuiTransfers' WHERE SystemCode='FengShuiTransfer'");
            Sql("UPDATE PlotItems SET SystemCode='Reciprocates' WHERE SystemCode='Reciprocate'");
            Sql("UPDATE PlotItems SET SystemCode='SecondBurials' WHERE SystemCode='SecondBurial'");
        }
        
        public override void Down()
        {
        }
    }
}
