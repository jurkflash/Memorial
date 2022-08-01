namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateToPlotlandscapeCompaniesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlotLandscapeCompanies", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlotLandscapeCompanies", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.PlotLandscapeCompanies", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlotLandscapeCompanies", "DeleteDate");
            DropColumn("dbo.PlotLandscapeCompanies", "ModifyDate");
            DropColumn("dbo.PlotLandscapeCompanies", "CreateDate");
        }
    }
}
