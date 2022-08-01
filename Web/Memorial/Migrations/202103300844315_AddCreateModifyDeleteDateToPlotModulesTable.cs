namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateToPlotModulesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotTransactions", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.PlotTransactions", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.Plots", "hasDeceased", c => c.Boolean(nullable: false));
            AddColumn("dbo.Plots", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Plots", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Plots", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.PlotAreas", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlotAreas", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.PlotAreas", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.PlotItems", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlotItems", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.PlotItems", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlotItems", "DeleteDate");
            DropColumn("dbo.PlotItems", "ModifyDate");
            DropColumn("dbo.PlotItems", "CreateDate");
            DropColumn("dbo.PlotAreas", "DeleteDate");
            DropColumn("dbo.PlotAreas", "ModifyDate");
            DropColumn("dbo.PlotAreas", "CreateDate");
            DropColumn("dbo.Plots", "DeleteDate");
            DropColumn("dbo.Plots", "ModifyDate");
            DropColumn("dbo.Plots", "CreateDate");
            DropColumn("dbo.Plots", "hasDeceased");
            DropColumn("dbo.PlotTransactions", "DeleteDate");
            DropColumn("dbo.PlotTransactions", "ModifyDate");
        }
    }
}
