﻿namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditById : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AncestralTabletAreas", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTabletAreas", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTabletAreas", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTabletItems", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTabletItems", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTabletItems", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTabletTransactions", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTabletTransactions", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTabletTransactions", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTablets", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTablets", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.AncestralTablets", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Applicants", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Applicants", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Applicants", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicantDeceaseds", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicantDeceaseds", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicantDeceaseds", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Deceaseds", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Deceaseds", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Deceaseds", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryTransactions", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryTransactions", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryTransactions", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryItems", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryItems", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryItems", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Plots", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Plots", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Plots", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryAreas", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryAreas", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryAreas", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Sites", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Sites", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Sites", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Catalogs", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Catalogs", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Catalogs", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumItems", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumItems", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumItems", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumCentres", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumCentres", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumCentres", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumAreas", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumAreas", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumAreas", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Niches", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Niches", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Niches", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumTransactions", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumTransactions", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.ColumbariumTransactions", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.FuneralCompanies", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.FuneralCompanies", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.FuneralCompanies", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.CremationItems", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.CremationItems", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.CremationItems", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Cremations", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Cremations", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Cremations", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryLandscapeCompanies", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryLandscapeCompanies", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.CemeteryLandscapeCompanies", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.MiscellaneousItems", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.MiscellaneousItems", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.MiscellaneousItems", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Miscellaneous", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Miscellaneous", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Miscellaneous", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Receipts", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Receipts", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Receipts", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.SpaceTransactions", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.SpaceTransactions", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.SpaceTransactions", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.SpaceItems", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.SpaceItems", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.SpaceItems", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Spaces", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Spaces", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Spaces", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.UrnTransactions", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.UrnTransactions", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.UrnTransactions", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.UrnItems", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.UrnItems", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.UrnItems", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.Urns", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Urns", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.Urns", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.NicheTypes", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.NicheTypes", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.NicheTypes", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.PlotTypes", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.PlotTypes", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.PlotTypes", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.FengShuiMasters", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.FengShuiMasters", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.FengShuiMasters", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.GenderTypes", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.GenderTypes", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.GenderTypes", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.MaritalTypes", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.MaritalTypes", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.MaritalTypes", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.NationalityTypes", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.NationalityTypes", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.NationalityTypes", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.ReligionTypes", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.ReligionTypes", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.ReligionTypes", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.RelationshipTypes", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.RelationshipTypes", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.RelationshipTypes", "DeletedById", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RelationshipTypes", "DeletedById");
            DropColumn("dbo.RelationshipTypes", "ModifiedById");
            DropColumn("dbo.RelationshipTypes", "CreatedById");
            DropColumn("dbo.ReligionTypes", "DeletedById");
            DropColumn("dbo.ReligionTypes", "ModifiedById");
            DropColumn("dbo.ReligionTypes", "CreatedById");
            DropColumn("dbo.NationalityTypes", "DeletedById");
            DropColumn("dbo.NationalityTypes", "ModifiedById");
            DropColumn("dbo.NationalityTypes", "CreatedById");
            DropColumn("dbo.MaritalTypes", "DeletedById");
            DropColumn("dbo.MaritalTypes", "ModifiedById");
            DropColumn("dbo.MaritalTypes", "CreatedById");
            DropColumn("dbo.GenderTypes", "DeletedById");
            DropColumn("dbo.GenderTypes", "ModifiedById");
            DropColumn("dbo.GenderTypes", "CreatedById");
            DropColumn("dbo.FengShuiMasters", "DeletedById");
            DropColumn("dbo.FengShuiMasters", "ModifiedById");
            DropColumn("dbo.FengShuiMasters", "CreatedById");
            DropColumn("dbo.PlotTypes", "DeletedById");
            DropColumn("dbo.PlotTypes", "ModifiedById");
            DropColumn("dbo.PlotTypes", "CreatedById");
            DropColumn("dbo.NicheTypes", "DeletedById");
            DropColumn("dbo.NicheTypes", "ModifiedById");
            DropColumn("dbo.NicheTypes", "CreatedById");
            DropColumn("dbo.Urns", "DeletedById");
            DropColumn("dbo.Urns", "ModifiedById");
            DropColumn("dbo.Urns", "CreatedById");
            DropColumn("dbo.UrnItems", "DeletedById");
            DropColumn("dbo.UrnItems", "ModifiedById");
            DropColumn("dbo.UrnItems", "CreatedById");
            DropColumn("dbo.UrnTransactions", "DeletedById");
            DropColumn("dbo.UrnTransactions", "ModifiedById");
            DropColumn("dbo.UrnTransactions", "CreatedById");
            DropColumn("dbo.Spaces", "DeletedById");
            DropColumn("dbo.Spaces", "ModifiedById");
            DropColumn("dbo.Spaces", "CreatedById");
            DropColumn("dbo.SpaceItems", "DeletedById");
            DropColumn("dbo.SpaceItems", "ModifiedById");
            DropColumn("dbo.SpaceItems", "CreatedById");
            DropColumn("dbo.SpaceTransactions", "DeletedById");
            DropColumn("dbo.SpaceTransactions", "ModifiedById");
            DropColumn("dbo.SpaceTransactions", "CreatedById");
            DropColumn("dbo.Receipts", "DeletedById");
            DropColumn("dbo.Receipts", "ModifiedById");
            DropColumn("dbo.Receipts", "CreatedById");
            DropColumn("dbo.Miscellaneous", "DeletedById");
            DropColumn("dbo.Miscellaneous", "ModifiedById");
            DropColumn("dbo.Miscellaneous", "CreatedById");
            DropColumn("dbo.MiscellaneousItems", "DeletedById");
            DropColumn("dbo.MiscellaneousItems", "ModifiedById");
            DropColumn("dbo.MiscellaneousItems", "CreatedById");
            DropColumn("dbo.CemeteryLandscapeCompanies", "DeletedById");
            DropColumn("dbo.CemeteryLandscapeCompanies", "ModifiedById");
            DropColumn("dbo.CemeteryLandscapeCompanies", "CreatedById");
            DropColumn("dbo.Invoices", "DeletedById");
            DropColumn("dbo.Invoices", "ModifiedById");
            DropColumn("dbo.Invoices", "CreatedById");
            DropColumn("dbo.Cremations", "DeletedById");
            DropColumn("dbo.Cremations", "ModifiedById");
            DropColumn("dbo.Cremations", "CreatedById");
            DropColumn("dbo.CremationItems", "DeletedById");
            DropColumn("dbo.CremationItems", "ModifiedById");
            DropColumn("dbo.CremationItems", "CreatedById");
            DropColumn("dbo.FuneralCompanies", "DeletedById");
            DropColumn("dbo.FuneralCompanies", "ModifiedById");
            DropColumn("dbo.FuneralCompanies", "CreatedById");
            DropColumn("dbo.ColumbariumTransactions", "DeletedById");
            DropColumn("dbo.ColumbariumTransactions", "ModifiedById");
            DropColumn("dbo.ColumbariumTransactions", "CreatedById");
            DropColumn("dbo.Niches", "DeletedById");
            DropColumn("dbo.Niches", "ModifiedById");
            DropColumn("dbo.Niches", "CreatedById");
            DropColumn("dbo.ColumbariumAreas", "DeletedById");
            DropColumn("dbo.ColumbariumAreas", "ModifiedById");
            DropColumn("dbo.ColumbariumAreas", "CreatedById");
            DropColumn("dbo.ColumbariumCentres", "DeletedById");
            DropColumn("dbo.ColumbariumCentres", "ModifiedById");
            DropColumn("dbo.ColumbariumCentres", "CreatedById");
            DropColumn("dbo.ColumbariumItems", "DeletedById");
            DropColumn("dbo.ColumbariumItems", "ModifiedById");
            DropColumn("dbo.ColumbariumItems", "CreatedById");
            DropColumn("dbo.Catalogs", "DeletedById");
            DropColumn("dbo.Catalogs", "ModifiedById");
            DropColumn("dbo.Catalogs", "CreatedById");
            DropColumn("dbo.Sites", "DeletedById");
            DropColumn("dbo.Sites", "ModifiedById");
            DropColumn("dbo.Sites", "CreatedById");
            DropColumn("dbo.CemeteryAreas", "DeletedById");
            DropColumn("dbo.CemeteryAreas", "ModifiedById");
            DropColumn("dbo.CemeteryAreas", "CreatedById");
            DropColumn("dbo.Plots", "DeletedById");
            DropColumn("dbo.Plots", "ModifiedById");
            DropColumn("dbo.Plots", "CreatedById");
            DropColumn("dbo.CemeteryItems", "DeletedById");
            DropColumn("dbo.CemeteryItems", "ModifiedById");
            DropColumn("dbo.CemeteryItems", "CreatedById");
            DropColumn("dbo.CemeteryTransactions", "DeletedById");
            DropColumn("dbo.CemeteryTransactions", "ModifiedById");
            DropColumn("dbo.CemeteryTransactions", "CreatedById");
            DropColumn("dbo.Deceaseds", "DeletedById");
            DropColumn("dbo.Deceaseds", "ModifiedById");
            DropColumn("dbo.Deceaseds", "CreatedById");
            DropColumn("dbo.ApplicantDeceaseds", "DeletedById");
            DropColumn("dbo.ApplicantDeceaseds", "ModifiedById");
            DropColumn("dbo.ApplicantDeceaseds", "CreatedById");
            DropColumn("dbo.Applicants", "DeletedById");
            DropColumn("dbo.Applicants", "ModifiedById");
            DropColumn("dbo.Applicants", "CreatedById");
            DropColumn("dbo.AncestralTablets", "DeletedById");
            DropColumn("dbo.AncestralTablets", "ModifiedById");
            DropColumn("dbo.AncestralTablets", "CreatedById");
            DropColumn("dbo.AncestralTabletTransactions", "DeletedById");
            DropColumn("dbo.AncestralTabletTransactions", "ModifiedById");
            DropColumn("dbo.AncestralTabletTransactions", "CreatedById");
            DropColumn("dbo.AncestralTabletItems", "DeletedById");
            DropColumn("dbo.AncestralTabletItems", "ModifiedById");
            DropColumn("dbo.AncestralTabletItems", "CreatedById");
            DropColumn("dbo.AncestralTabletAreas", "DeletedById");
            DropColumn("dbo.AncestralTabletAreas", "ModifiedById");
            DropColumn("dbo.AncestralTabletAreas", "CreatedById");
        }
    }
}
