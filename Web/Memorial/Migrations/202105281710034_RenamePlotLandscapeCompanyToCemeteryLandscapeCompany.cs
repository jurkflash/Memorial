namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlotLandscapeCompanyToCemeteryLandscapeCompany : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlotLandscapeCompanies", newName: "CemeteryLandscapeCompanies");
            RenameColumn(table: "dbo.MiscellaneousTransactions", name: "PlotLandscapeCompanyId", newName: "CemeteryLandscapeCompanyId");
            RenameIndex(table: "dbo.MiscellaneousTransactions", name: "IX_PlotLandscapeCompanyId", newName: "IX_CemeteryLandscapeCompanyId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MiscellaneousTransactions", name: "IX_CemeteryLandscapeCompanyId", newName: "IX_PlotLandscapeCompanyId");
            RenameColumn(table: "dbo.MiscellaneousTransactions", name: "CemeteryLandscapeCompanyId", newName: "PlotLandscapeCompanyId");
            RenameTable(name: "dbo.CemeteryLandscapeCompanies", newName: "PlotLandscapeCompanies");
        }
    }
}
