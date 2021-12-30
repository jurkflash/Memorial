namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivteStatusForAllBaseInherited : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestralTabletAreas", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.AncestralTabletItems", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.AncestralTabletTransactions", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.AncestralTablets", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicants", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicantDeceaseds", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Deceaseds", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.CemeteryTransactions", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.CemeteryItems", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Plots", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.CemeteryAreas", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sites", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Catalogs", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.ColumbariumItems", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.ColumbariumCentres", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.ColumbariumAreas", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Niches", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.ColumbariumTransactions", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.FuneralCompanies", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.CremationItems", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cremations", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Invoices", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.CemeteryLandscapeCompanies", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.MiscellaneousItems", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Miscellaneous", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Receipts", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.SpaceTransactions", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.SpaceItems", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Spaces", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.UrnTransactions", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.UrnItems", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Urns", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.NicheTypes", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlotTypes", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.FengShuiMasters", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.GenderTypes", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.MaritalTypes", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.NationalityTypes", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReligionTypes", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.RelationshipTypes", "ActiveStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RelationshipTypes", "ActiveStatus");
            DropColumn("dbo.ReligionTypes", "ActiveStatus");
            DropColumn("dbo.NationalityTypes", "ActiveStatus");
            DropColumn("dbo.MaritalTypes", "ActiveStatus");
            DropColumn("dbo.GenderTypes", "ActiveStatus");
            DropColumn("dbo.FengShuiMasters", "ActiveStatus");
            DropColumn("dbo.PlotTypes", "ActiveStatus");
            DropColumn("dbo.NicheTypes", "ActiveStatus");
            DropColumn("dbo.Urns", "ActiveStatus");
            DropColumn("dbo.UrnItems", "ActiveStatus");
            DropColumn("dbo.UrnTransactions", "ActiveStatus");
            DropColumn("dbo.Spaces", "ActiveStatus");
            DropColumn("dbo.SpaceItems", "ActiveStatus");
            DropColumn("dbo.SpaceTransactions", "ActiveStatus");
            DropColumn("dbo.Receipts", "ActiveStatus");
            DropColumn("dbo.Miscellaneous", "ActiveStatus");
            DropColumn("dbo.MiscellaneousItems", "ActiveStatus");
            DropColumn("dbo.CemeteryLandscapeCompanies", "ActiveStatus");
            DropColumn("dbo.Invoices", "ActiveStatus");
            DropColumn("dbo.Cremations", "ActiveStatus");
            DropColumn("dbo.CremationItems", "ActiveStatus");
            DropColumn("dbo.FuneralCompanies", "ActiveStatus");
            DropColumn("dbo.ColumbariumTransactions", "ActiveStatus");
            DropColumn("dbo.Niches", "ActiveStatus");
            DropColumn("dbo.ColumbariumAreas", "ActiveStatus");
            DropColumn("dbo.ColumbariumCentres", "ActiveStatus");
            DropColumn("dbo.ColumbariumItems", "ActiveStatus");
            DropColumn("dbo.Catalogs", "ActiveStatus");
            DropColumn("dbo.Sites", "ActiveStatus");
            DropColumn("dbo.CemeteryAreas", "ActiveStatus");
            DropColumn("dbo.Plots", "ActiveStatus");
            DropColumn("dbo.CemeteryItems", "ActiveStatus");
            DropColumn("dbo.CemeteryTransactions", "ActiveStatus");
            DropColumn("dbo.Deceaseds", "ActiveStatus");
            DropColumn("dbo.ApplicantDeceaseds", "ActiveStatus");
            DropColumn("dbo.Applicants", "ActiveStatus");
            DropColumn("dbo.AncestralTablets", "ActiveStatus");
            DropColumn("dbo.AncestralTabletTransactions", "ActiveStatus");
            DropColumn("dbo.AncestralTabletItems", "ActiveStatus");
            DropColumn("dbo.AncestralTabletAreas", "ActiveStatus");
        }
    }
}
