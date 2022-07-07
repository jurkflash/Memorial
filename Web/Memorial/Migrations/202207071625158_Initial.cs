namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessControls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AncestralTablet = c.Byte(nullable: false),
                        Cemetery = c.Byte(nullable: false),
                        Columbarium = c.Byte(nullable: false),
                        Cremation = c.Byte(nullable: false),
                        Miscellaneous = c.Byte(nullable: false),
                        Urn = c.Byte(nullable: false),
                        Space = c.Byte(nullable: false),
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AncestralTabletAreas",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        SiteId = c.Int(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.AncestralTabletItems",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(),
                        Code = c.String(maxLength: 10),
                        isOrder = c.Boolean(),
                        SubProductServiceId = c.Int(nullable: false),
                        AncestralTabletAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AncestralTabletAreas", t => t.AncestralTabletAreaId)
                .ForeignKey("dbo.SubProductServices", t => t.SubProductServiceId)
                .Index(t => t.SubProductServiceId)
                .Index(t => t.AncestralTabletAreaId);
            
            CreateTable(
                "dbo.AncestralTabletTransactions",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        AF = c.String(nullable: false, maxLength: 50),
                        AncestralTabletItemId = c.Int(nullable: false),
                        AncestralTabletId = c.Int(nullable: false),
                        Label = c.String(),
                        Remark = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(),
                        ApplicantId = c.Int(nullable: false),
                        DeceasedId = c.Int(),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        ShiftedAncestralTabletId = c.Int(),
                        ShiftedAncestralTabletTransactionAF = c.String(maxLength: 50),
                        WithdrewAFS = c.String(),
                        WithdrewAncestralTabletApplicantId = c.Int(),
                        SummaryItem = c.String(),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.AncestralTablets", t => t.AncestralTabletId)
                .ForeignKey("dbo.AncestralTabletItems", t => t.AncestralTabletItemId)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .ForeignKey("dbo.AncestralTablets", t => t.ShiftedAncestralTabletId)
                .ForeignKey("dbo.AncestralTabletTransactions", t => t.ShiftedAncestralTabletTransactionAF)
                .Index(t => t.AncestralTabletItemId)
                .Index(t => t.AncestralTabletId)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId)
                .Index(t => t.ShiftedAncestralTabletId)
                .Index(t => t.ShiftedAncestralTabletTransactionAF);
            
            CreateTable(
                "dbo.AncestralTablets",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        PositionX = c.Byte(nullable: false),
                        PositionY = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        ApplicantId = c.Int(),
                        AncestralTabletAreaId = c.Int(nullable: false),
                        hasDeceased = c.Boolean(nullable: false),
                        hasFreeOrder = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AncestralTabletAreas", t => t.AncestralTabletAreaId)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .Index(t => t.ApplicantId)
                .Index(t => t.AncestralTabletAreaId);
            
            CreateTable(
                "dbo.AncestralTabletTrackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AncestralTabletId = c.Int(nullable: false),
                        AncestralTabletTransactionAF = c.String(nullable: false, maxLength: 50),
                        ApplicantId = c.Int(),
                        DeceasedId = c.Int(),
                        ToDeleteFlag = c.Boolean(nullable: false),
                        ActionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AncestralTablets", t => t.AncestralTabletId)
                .ForeignKey("dbo.AncestralTabletTransactions", t => t.AncestralTabletTransactionAF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .Index(t => t.AncestralTabletId)
                .Index(t => t.AncestralTabletTransactionAF)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId);
            
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        IC = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 255),
                        Name2 = c.String(),
                        Address = c.String(),
                        HousePhone = c.String(),
                        MobileNumber = c.String(),
                        Remark = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicantDeceaseds",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        DeceasedId = c.Int(nullable: false),
                        RelationshipTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .ForeignKey("dbo.RelationshipTypes", t => t.RelationshipTypeId)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId)
                .Index(t => t.RelationshipTypeId);
            
            CreateTable(
                "dbo.Deceaseds",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        IC = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 255),
                        Name2 = c.String(),
                        Age = c.Byte(nullable: false),
                        Address = c.String(),
                        Remark = c.String(maxLength: 255),
                        GenderTypeId = c.Byte(nullable: false),
                        Province = c.String(),
                        NationalityTypeId = c.Byte(nullable: false),
                        MaritalTypeId = c.Byte(nullable: false),
                        ReligionTypeId = c.Byte(nullable: false),
                        DeathPlace = c.String(),
                        DeathDate = c.DateTime(nullable: false),
                        DeathRegistrationCentre = c.String(),
                        DeathCertificate = c.String(),
                        BurialCertificate = c.String(),
                        ImportPermitNumber = c.String(),
                        NicheId = c.Int(),
                        PlotId = c.Int(),
                        AncestralTabletId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Niches", t => t.NicheId)
                .ForeignKey("dbo.Plots", t => t.PlotId)
                .ForeignKey("dbo.GenderTypes", t => t.GenderTypeId)
                .ForeignKey("dbo.MaritalTypes", t => t.MaritalTypeId)
                .ForeignKey("dbo.NationalityTypes", t => t.NationalityTypeId)
                .ForeignKey("dbo.ReligionTypes", t => t.ReligionTypeId)
                .ForeignKey("dbo.AncestralTablets", t => t.AncestralTabletId)
                .Index(t => t.GenderTypeId)
                .Index(t => t.NationalityTypeId)
                .Index(t => t.MaritalTypeId)
                .Index(t => t.ReligionTypeId)
                .Index(t => t.NicheId)
                .Index(t => t.PlotId)
                .Index(t => t.AncestralTabletId);
            
            CreateTable(
                "dbo.CemeteryTrackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlotId = c.Int(nullable: false),
                        CemeteryTransactionAF = c.String(nullable: false, maxLength: 50),
                        ApplicantId = c.Int(),
                        Deceased1Id = c.Int(),
                        Deceased2Id = c.Int(),
                        Deceased3Id = c.Int(),
                        ActionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.CemeteryTransactions", t => t.CemeteryTransactionAF)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased1Id)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased2Id)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased3Id)
                .ForeignKey("dbo.Plots", t => t.PlotId)
                .Index(t => t.PlotId)
                .Index(t => t.CemeteryTransactionAF)
                .Index(t => t.ApplicantId)
                .Index(t => t.Deceased1Id)
                .Index(t => t.Deceased2Id)
                .Index(t => t.Deceased3Id);
            
            CreateTable(
                "dbo.CemeteryTransactions",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        AF = c.String(nullable: false, maxLength: 50),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(),
                        Wall = c.Single(),
                        Dig = c.Single(),
                        Brick = c.Single(),
                        Total = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        CemeteryItemId = c.Int(nullable: false),
                        PlotId = c.Int(nullable: false),
                        FengShuiMasterId = c.Int(),
                        FuneralCompanyId = c.Int(),
                        ApplicantId = c.Int(nullable: false),
                        Deceased1Id = c.Int(),
                        Deceased2Id = c.Int(),
                        Deceased3Id = c.Int(),
                        ClearedApplicantId = c.Int(),
                        TransferredApplicantId = c.Int(),
                        TransferredCemeteryTransactionAF = c.String(maxLength: 50),
                        SummaryItem = c.String(),
                        ClearanceDate = c.DateTime(),
                        ClearanceGroundDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.CemeteryItems", t => t.CemeteryItemId)
                .ForeignKey("dbo.Applicants", t => t.ClearedApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased1Id)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased2Id)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased3Id)
                .ForeignKey("dbo.FengShuiMasters", t => t.FengShuiMasterId)
                .ForeignKey("dbo.FuneralCompanies", t => t.FuneralCompanyId)
                .ForeignKey("dbo.Plots", t => t.PlotId)
                .ForeignKey("dbo.Applicants", t => t.TransferredApplicantId)
                .ForeignKey("dbo.CemeteryTransactions", t => t.TransferredCemeteryTransactionAF)
                .Index(t => t.CemeteryItemId)
                .Index(t => t.PlotId)
                .Index(t => t.FengShuiMasterId)
                .Index(t => t.FuneralCompanyId)
                .Index(t => t.ApplicantId)
                .Index(t => t.Deceased1Id)
                .Index(t => t.Deceased2Id)
                .Index(t => t.Deceased3Id)
                .Index(t => t.ClearedApplicantId)
                .Index(t => t.TransferredApplicantId)
                .Index(t => t.TransferredCemeteryTransactionAF);
            
            CreateTable(
                "dbo.CemeteryItems",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(),
                        Code = c.String(maxLength: 10),
                        isOrder = c.Boolean(),
                        PlotId = c.Int(nullable: false),
                        SubProductServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plots", t => t.PlotId)
                .ForeignKey("dbo.SubProductServices", t => t.SubProductServiceId)
                .Index(t => t.PlotId)
                .Index(t => t.SubProductServiceId);
            
            CreateTable(
                "dbo.Plots",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        Size = c.String(nullable: false, maxLength: 255),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(nullable: false),
                        Wall = c.Single(nullable: false),
                        Dig = c.Single(nullable: false),
                        Brick = c.Single(nullable: false),
                        Remark = c.String(),
                        PlotTypeId = c.Byte(nullable: false),
                        CemeteryAreaId = c.Int(nullable: false),
                        ApplicantId = c.Int(),
                        hasDeceased = c.Boolean(nullable: false),
                        hasCleared = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.CemeteryAreas", t => t.CemeteryAreaId)
                .ForeignKey("dbo.PlotTypes", t => t.PlotTypeId)
                .Index(t => t.PlotTypeId)
                .Index(t => t.CemeteryAreaId)
                .Index(t => t.ApplicantId);
            
            CreateTable(
                "dbo.CemeteryAreas",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Code = c.String(nullable: false, maxLength: 10),
                        Address = c.String(),
                        Header = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Catalogs",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.ProductId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Area = c.String(maxLength: 255),
                        Controller = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubProductServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        SystemCode = c.String(nullable: false, maxLength: 50),
                        isOrder = c.Boolean(nullable: false),
                        OtherId = c.Int(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ColumbariumItems",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(),
                        Code = c.String(maxLength: 10),
                        isOrder = c.Boolean(),
                        SubProductServiceId = c.Int(nullable: false),
                        ColumbariumCentreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ColumbariumCentres", t => t.ColumbariumCentreId)
                .ForeignKey("dbo.SubProductServices", t => t.SubProductServiceId)
                .Index(t => t.SubProductServiceId)
                .Index(t => t.ColumbariumCentreId);
            
            CreateTable(
                "dbo.ColumbariumCentres",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.ColumbariumAreas",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        ColumbariumCentreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ColumbariumCentres", t => t.ColumbariumCentreId)
                .Index(t => t.ColumbariumCentreId);
            
            CreateTable(
                "dbo.Niches",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 100),
                        PositionX = c.Byte(nullable: false),
                        PositionY = c.Byte(nullable: false),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(nullable: false),
                        LifeTimeMaintenance = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        NicheTypeId = c.Byte(nullable: false),
                        ColumbariumAreaId = c.Int(nullable: false),
                        ApplicantId = c.Int(),
                        hasDeceased = c.Boolean(nullable: false),
                        hasFreeOrder = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.ColumbariumAreas", t => t.ColumbariumAreaId)
                .ForeignKey("dbo.NicheTypes", t => t.NicheTypeId)
                .Index(t => t.NicheTypeId)
                .Index(t => t.ColumbariumAreaId)
                .Index(t => t.ApplicantId);
            
            CreateTable(
                "dbo.ColumbariumTrackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NicheId = c.Int(nullable: false),
                        ColumbariumTransactionAF = c.String(nullable: false, maxLength: 50),
                        ApplicantId = c.Int(),
                        Deceased1Id = c.Int(),
                        Deceased2Id = c.Int(),
                        ToDeleteFlag = c.Boolean(nullable: false),
                        ActionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.ColumbariumTransactions", t => t.ColumbariumTransactionAF)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased1Id)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased2Id)
                .ForeignKey("dbo.Niches", t => t.NicheId)
                .Index(t => t.NicheId)
                .Index(t => t.ColumbariumTransactionAF)
                .Index(t => t.ApplicantId)
                .Index(t => t.Deceased1Id)
                .Index(t => t.Deceased2Id);
            
            CreateTable(
                "dbo.ColumbariumTransactions",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        AF = c.String(nullable: false, maxLength: 50),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(),
                        LifeTimeMaintenance = c.Single(),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        Text1 = c.String(),
                        Text2 = c.String(),
                        Text3 = c.String(),
                        Remark = c.String(maxLength: 255),
                        ColumbariumItemId = c.Int(nullable: false),
                        NicheId = c.Int(nullable: false),
                        FuneralCompanyId = c.Int(),
                        ApplicantId = c.Int(nullable: false),
                        Deceased1Id = c.Int(),
                        Deceased2Id = c.Int(),
                        ShiftedNicheId = c.Int(),
                        TransferredFromApplicantId = c.Int(),
                        ShiftedColumbariumTransactionAF = c.String(maxLength: 50),
                        TransferredApplicantId = c.Int(),
                        TransferredColumbariumTransactionAF = c.String(maxLength: 50),
                        WithdrewAFS = c.String(),
                        WithdrewColumbariumApplicantId = c.Int(),
                        SummaryItem = c.String(),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.ColumbariumItems", t => t.ColumbariumItemId)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased1Id)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased2Id)
                .ForeignKey("dbo.FuneralCompanies", t => t.FuneralCompanyId)
                .ForeignKey("dbo.Niches", t => t.NicheId)
                .ForeignKey("dbo.ColumbariumTransactions", t => t.ShiftedColumbariumTransactionAF)
                .ForeignKey("dbo.Niches", t => t.ShiftedNicheId)
                .ForeignKey("dbo.Applicants", t => t.TransferredApplicantId)
                .ForeignKey("dbo.ColumbariumTransactions", t => t.TransferredColumbariumTransactionAF)
                .ForeignKey("dbo.Applicants", t => t.TransferredFromApplicantId)
                .Index(t => t.ColumbariumItemId)
                .Index(t => t.NicheId)
                .Index(t => t.FuneralCompanyId)
                .Index(t => t.ApplicantId)
                .Index(t => t.Deceased1Id)
                .Index(t => t.Deceased2Id)
                .Index(t => t.ShiftedNicheId)
                .Index(t => t.TransferredFromApplicantId)
                .Index(t => t.ShiftedColumbariumTransactionAF)
                .Index(t => t.TransferredApplicantId)
                .Index(t => t.TransferredColumbariumTransactionAF);
            
            CreateTable(
                "dbo.FuneralCompanies",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        ContactPerson = c.String(maxLength: 255),
                        ContactNumber = c.String(maxLength: 255),
                        Remark = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CremationTransactions",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        AF = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        CremationItemId = c.Int(nullable: false),
                        CremateDate = c.DateTime(nullable: false),
                        FuneralCompanyId = c.Int(),
                        ApplicantId = c.Int(nullable: false),
                        DeceasedId = c.Int(nullable: false),
                        SummaryItem = c.String(),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.CremationItems", t => t.CremationItemId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .ForeignKey("dbo.FuneralCompanies", t => t.FuneralCompanyId)
                .Index(t => t.CremationItemId)
                .Index(t => t.FuneralCompanyId)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId);
            
            CreateTable(
                "dbo.CremationItems",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(),
                        Code = c.String(maxLength: 10),
                        CremationId = c.Int(nullable: false),
                        isOrder = c.Boolean(),
                        SubProductServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cremations", t => t.CremationId)
                .ForeignKey("dbo.SubProductServices", t => t.SubProductServiceId)
                .Index(t => t.CremationId)
                .Index(t => t.SubProductServiceId);
            
            CreateTable(
                "dbo.Cremations",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        IV = c.String(nullable: false, maxLength: 50),
                        Amount = c.Single(nullable: false),
                        isPaid = c.Boolean(nullable: false),
                        hasReceipt = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 255),
                        CemeteryTransactionAF = c.String(maxLength: 50),
                        CremationTransactionAF = c.String(maxLength: 50),
                        AncestralTabletTransactionAF = c.String(maxLength: 50),
                        MiscellaneousTransactionAF = c.String(maxLength: 50),
                        ColumbariumTransactionAF = c.String(maxLength: 50),
                        SpaceTransactionAF = c.String(maxLength: 50),
                        UrnTransactionAF = c.String(maxLength: 50),
                        AllowDeposit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IV)
                .ForeignKey("dbo.AncestralTabletTransactions", t => t.AncestralTabletTransactionAF)
                .ForeignKey("dbo.CemeteryTransactions", t => t.CemeteryTransactionAF)
                .ForeignKey("dbo.ColumbariumTransactions", t => t.ColumbariumTransactionAF)
                .ForeignKey("dbo.CremationTransactions", t => t.CremationTransactionAF)
                .ForeignKey("dbo.MiscellaneousTransactions", t => t.MiscellaneousTransactionAF)
                .ForeignKey("dbo.SpaceTransactions", t => t.SpaceTransactionAF)
                .ForeignKey("dbo.UrnTransactions", t => t.UrnTransactionAF)
                .Index(t => t.CemeteryTransactionAF)
                .Index(t => t.CremationTransactionAF)
                .Index(t => t.AncestralTabletTransactionAF)
                .Index(t => t.MiscellaneousTransactionAF)
                .Index(t => t.ColumbariumTransactionAF)
                .Index(t => t.SpaceTransactionAF)
                .Index(t => t.UrnTransactionAF);
            
            CreateTable(
                "dbo.MiscellaneousTransactions",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        AF = c.String(nullable: false, maxLength: 50),
                        BasePrice = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        MiscellaneousItemId = c.Int(nullable: false),
                        ApplicantId = c.Int(),
                        CemeteryLandscapeCompanyId = c.Int(),
                        SummaryItem = c.String(),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.CemeteryLandscapeCompanies", t => t.CemeteryLandscapeCompanyId)
                .ForeignKey("dbo.MiscellaneousItems", t => t.MiscellaneousItemId)
                .Index(t => t.MiscellaneousItemId)
                .Index(t => t.ApplicantId)
                .Index(t => t.CemeteryLandscapeCompanyId);
            
            CreateTable(
                "dbo.CemeteryLandscapeCompanies",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        ContactPerson = c.String(maxLength: 255),
                        ContactNumber = c.String(maxLength: 255),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MiscellaneousItems",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(),
                        Code = c.String(maxLength: 10),
                        isOrder = c.Boolean(),
                        MiscellaneousId = c.Int(nullable: false),
                        SubProductServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Miscellaneous", t => t.MiscellaneousId)
                .ForeignKey("dbo.SubProductServices", t => t.SubProductServiceId)
                .Index(t => t.MiscellaneousId)
                .Index(t => t.SubProductServiceId);
            
            CreateTable(
                "dbo.Miscellaneous",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        Remark = c.String(maxLength: 255),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        RE = c.String(nullable: false, maxLength: 50),
                        InvoiceIV = c.String(maxLength: 50),
                        CemeteryTransactionAF = c.String(maxLength: 50),
                        CremationTransactionAF = c.String(maxLength: 50),
                        AncestralTabletTransactionAF = c.String(maxLength: 50),
                        MiscellaneousTransactionAF = c.String(maxLength: 50),
                        ColumbariumTransactionAF = c.String(maxLength: 50),
                        SpaceTransactionAF = c.String(maxLength: 50),
                        UrnTransactionAF = c.String(maxLength: 50),
                        Amount = c.Single(nullable: false),
                        PaymentMethodId = c.Byte(nullable: false),
                        PaymentRemark = c.String(),
                        Remark = c.String(maxLength: 255),
                        isDeposit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RE)
                .ForeignKey("dbo.AncestralTabletTransactions", t => t.AncestralTabletTransactionAF)
                .ForeignKey("dbo.CemeteryTransactions", t => t.CemeteryTransactionAF)
                .ForeignKey("dbo.ColumbariumTransactions", t => t.ColumbariumTransactionAF)
                .ForeignKey("dbo.CremationTransactions", t => t.CremationTransactionAF)
                .ForeignKey("dbo.Invoices", t => t.InvoiceIV)
                .ForeignKey("dbo.MiscellaneousTransactions", t => t.MiscellaneousTransactionAF)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodId)
                .ForeignKey("dbo.SpaceTransactions", t => t.SpaceTransactionAF)
                .ForeignKey("dbo.UrnTransactions", t => t.UrnTransactionAF)
                .Index(t => t.InvoiceIV)
                .Index(t => t.CemeteryTransactionAF)
                .Index(t => t.CremationTransactionAF)
                .Index(t => t.AncestralTabletTransactionAF)
                .Index(t => t.MiscellaneousTransactionAF)
                .Index(t => t.ColumbariumTransactionAF)
                .Index(t => t.SpaceTransactionAF)
                .Index(t => t.UrnTransactionAF)
                .Index(t => t.PaymentMethodId);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        RequireRemark = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpaceTransactions",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        AF = c.String(nullable: false, maxLength: 50),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        BasePrice = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                        OtherCharges = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        SpaceItemId = c.Int(nullable: false),
                        FuneralCompanyId = c.Int(),
                        ApplicantId = c.Int(nullable: false),
                        DeceasedId = c.Int(),
                        SummaryItem = c.String(),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Deceaseds", t => t.DeceasedId)
                .ForeignKey("dbo.FuneralCompanies", t => t.FuneralCompanyId)
                .ForeignKey("dbo.SpaceItems", t => t.SpaceItemId)
                .Index(t => t.SpaceItemId)
                .Index(t => t.FuneralCompanyId)
                .Index(t => t.ApplicantId)
                .Index(t => t.DeceasedId);
            
            CreateTable(
                "dbo.SpaceItems",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(),
                        Code = c.String(maxLength: 10),
                        isOrder = c.Boolean(),
                        AllowDeposit = c.Boolean(nullable: false),
                        AllowDoubleBook = c.Boolean(nullable: false),
                        ToleranceHour = c.Byte(nullable: false),
                        SpaceId = c.Int(nullable: false),
                        SubProductServiceId = c.Int(nullable: false),
                        FormView = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Spaces", t => t.SpaceId)
                .ForeignKey("dbo.SubProductServices", t => t.SubProductServiceId)
                .Index(t => t.SpaceId)
                .Index(t => t.SubProductServiceId);
            
            CreateTable(
                "dbo.Spaces",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Remark = c.String(maxLength: 255),
                        SiteId = c.Int(nullable: false),
                        ColorCode = c.String(maxLength: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.UrnTransactions",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        AF = c.String(nullable: false, maxLength: 50),
                        Price = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        UrnItemId = c.Int(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                        SummaryItem = c.String(),
                    })
                .PrimaryKey(t => t.AF)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.UrnItems", t => t.UrnItemId)
                .Index(t => t.UrnItemId)
                .Index(t => t.ApplicantId);
            
            CreateTable(
                "dbo.UrnItems",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(),
                        Code = c.String(maxLength: 10),
                        UrnId = c.Int(nullable: false),
                        isOrder = c.Boolean(),
                        SubProductServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubProductServices", t => t.SubProductServiceId)
                .ForeignKey("dbo.Urns", t => t.UrnId)
                .Index(t => t.UrnId)
                .Index(t => t.SubProductServiceId);
            
            CreateTable(
                "dbo.Urns",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Remark = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.NicheTypes",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        NumberOfPlacement = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlotTypes",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        NumberOfPlacement = c.Byte(nullable: false),
                        isFengShuiPlot = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FengShuiMasters",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        ContactPerson = c.String(maxLength: 255),
                        ContactNumber = c.String(maxLength: 255),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenderTypes",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaritalTypes",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NationalityTypes",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReligionTypes",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RelationshipTypes",
                c => new
                    {
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 40),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 40),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 40),
                        DeletedDate = c.DateTime(),
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AncestralTabletNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(nullable: false, maxLength: 10),
                        Year = c.Int(nullable: false),
                        AF = c.Int(nullable: false),
                        PO = c.Int(nullable: false),
                        IV = c.Int(nullable: false),
                        RE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CemeteryNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(nullable: false, maxLength: 10),
                        Year = c.Int(nullable: false),
                        AF = c.Int(nullable: false),
                        PO = c.Int(nullable: false),
                        IV = c.Int(nullable: false),
                        RE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ColumbariumNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(nullable: false, maxLength: 10),
                        Year = c.Int(nullable: false),
                        AF = c.Int(nullable: false),
                        PO = c.Int(nullable: false),
                        IV = c.Int(nullable: false),
                        RE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CremationNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(nullable: false, maxLength: 10),
                        Year = c.Int(nullable: false),
                        AF = c.Int(nullable: false),
                        PO = c.Int(nullable: false),
                        IV = c.Int(nullable: false),
                        RE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MiscellaneousNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(nullable: false, maxLength: 10),
                        Year = c.Int(nullable: false),
                        AF = c.Int(nullable: false),
                        PO = c.Int(nullable: false),
                        IV = c.Int(nullable: false),
                        RE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpaceNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(nullable: false, maxLength: 10),
                        Year = c.Int(nullable: false),
                        AF = c.Int(nullable: false),
                        PO = c.Int(nullable: false),
                        IV = c.Int(nullable: false),
                        RE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UrnNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(nullable: false, maxLength: 10),
                        Year = c.Int(nullable: false),
                        AF = c.Int(nullable: false),
                        PO = c.Int(nullable: false),
                        IV = c.Int(nullable: false),
                        RE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AncestralTabletAreas", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.AncestralTabletItems", "SubProductServiceId", "dbo.SubProductServices");
            DropForeignKey("dbo.AncestralTabletTransactions", "ShiftedAncestralTabletTransactionAF", "dbo.AncestralTabletTransactions");
            DropForeignKey("dbo.AncestralTabletTransactions", "ShiftedAncestralTabletId", "dbo.AncestralTablets");
            DropForeignKey("dbo.AncestralTabletTransactions", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.AncestralTabletTransactions", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.AncestralTabletTransactions", "AncestralTabletItemId", "dbo.AncestralTabletItems");
            DropForeignKey("dbo.AncestralTabletTransactions", "AncestralTabletId", "dbo.AncestralTablets");
            DropForeignKey("dbo.Deceaseds", "AncestralTabletId", "dbo.AncestralTablets");
            DropForeignKey("dbo.AncestralTablets", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.AncestralTabletTrackings", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.AncestralTabletTrackings", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ApplicantDeceaseds", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropForeignKey("dbo.ApplicantDeceaseds", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.Deceaseds", "ReligionTypeId", "dbo.ReligionTypes");
            DropForeignKey("dbo.Deceaseds", "NationalityTypeId", "dbo.NationalityTypes");
            DropForeignKey("dbo.Deceaseds", "MaritalTypeId", "dbo.MaritalTypes");
            DropForeignKey("dbo.Deceaseds", "GenderTypeId", "dbo.GenderTypes");
            DropForeignKey("dbo.CemeteryTrackings", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.CemeteryTrackings", "Deceased3Id", "dbo.Deceaseds");
            DropForeignKey("dbo.CemeteryTrackings", "Deceased2Id", "dbo.Deceaseds");
            DropForeignKey("dbo.CemeteryTrackings", "Deceased1Id", "dbo.Deceaseds");
            DropForeignKey("dbo.CemeteryTrackings", "CemeteryTransactionAF", "dbo.CemeteryTransactions");
            DropForeignKey("dbo.CemeteryTransactions", "TransferredCemeteryTransactionAF", "dbo.CemeteryTransactions");
            DropForeignKey("dbo.CemeteryTransactions", "TransferredApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.CemeteryTransactions", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.CemeteryTransactions", "FuneralCompanyId", "dbo.FuneralCompanies");
            DropForeignKey("dbo.CemeteryTransactions", "FengShuiMasterId", "dbo.FengShuiMasters");
            DropForeignKey("dbo.CemeteryTransactions", "Deceased3Id", "dbo.Deceaseds");
            DropForeignKey("dbo.CemeteryTransactions", "Deceased2Id", "dbo.Deceaseds");
            DropForeignKey("dbo.CemeteryTransactions", "Deceased1Id", "dbo.Deceaseds");
            DropForeignKey("dbo.CemeteryTransactions", "ClearedApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.CemeteryTransactions", "CemeteryItemId", "dbo.CemeteryItems");
            DropForeignKey("dbo.CemeteryItems", "SubProductServiceId", "dbo.SubProductServices");
            DropForeignKey("dbo.CemeteryItems", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.Plots", "PlotTypeId", "dbo.PlotTypes");
            DropForeignKey("dbo.Deceaseds", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.Plots", "CemeteryAreaId", "dbo.CemeteryAreas");
            DropForeignKey("dbo.CemeteryAreas", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Catalogs", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Catalogs", "ProductId", "dbo.Products");
            DropForeignKey("dbo.SubProductServices", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ColumbariumItems", "SubProductServiceId", "dbo.SubProductServices");
            DropForeignKey("dbo.ColumbariumItems", "ColumbariumCentreId", "dbo.ColumbariumCentres");
            DropForeignKey("dbo.ColumbariumCentres", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Niches", "NicheTypeId", "dbo.NicheTypes");
            DropForeignKey("dbo.Deceaseds", "NicheId", "dbo.Niches");
            DropForeignKey("dbo.ColumbariumTrackings", "NicheId", "dbo.Niches");
            DropForeignKey("dbo.ColumbariumTrackings", "Deceased2Id", "dbo.Deceaseds");
            DropForeignKey("dbo.ColumbariumTrackings", "Deceased1Id", "dbo.Deceaseds");
            DropForeignKey("dbo.ColumbariumTrackings", "ColumbariumTransactionAF", "dbo.ColumbariumTransactions");
            DropForeignKey("dbo.ColumbariumTransactions", "TransferredFromApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ColumbariumTransactions", "TransferredColumbariumTransactionAF", "dbo.ColumbariumTransactions");
            DropForeignKey("dbo.ColumbariumTransactions", "TransferredApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ColumbariumTransactions", "ShiftedNicheId", "dbo.Niches");
            DropForeignKey("dbo.ColumbariumTransactions", "ShiftedColumbariumTransactionAF", "dbo.ColumbariumTransactions");
            DropForeignKey("dbo.ColumbariumTransactions", "NicheId", "dbo.Niches");
            DropForeignKey("dbo.ColumbariumTransactions", "FuneralCompanyId", "dbo.FuneralCompanies");
            DropForeignKey("dbo.Invoices", "UrnTransactionAF", "dbo.UrnTransactions");
            DropForeignKey("dbo.Invoices", "SpaceTransactionAF", "dbo.SpaceTransactions");
            DropForeignKey("dbo.Invoices", "MiscellaneousTransactionAF", "dbo.MiscellaneousTransactions");
            DropForeignKey("dbo.Receipts", "UrnTransactionAF", "dbo.UrnTransactions");
            DropForeignKey("dbo.UrnTransactions", "UrnItemId", "dbo.UrnItems");
            DropForeignKey("dbo.UrnItems", "UrnId", "dbo.Urns");
            DropForeignKey("dbo.Urns", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.UrnItems", "SubProductServiceId", "dbo.SubProductServices");
            DropForeignKey("dbo.UrnTransactions", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Receipts", "SpaceTransactionAF", "dbo.SpaceTransactions");
            DropForeignKey("dbo.SpaceTransactions", "SpaceItemId", "dbo.SpaceItems");
            DropForeignKey("dbo.SpaceItems", "SubProductServiceId", "dbo.SubProductServices");
            DropForeignKey("dbo.SpaceItems", "SpaceId", "dbo.Spaces");
            DropForeignKey("dbo.Spaces", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.SpaceTransactions", "FuneralCompanyId", "dbo.FuneralCompanies");
            DropForeignKey("dbo.SpaceTransactions", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.SpaceTransactions", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Receipts", "PaymentMethodId", "dbo.PaymentMethods");
            DropForeignKey("dbo.Receipts", "MiscellaneousTransactionAF", "dbo.MiscellaneousTransactions");
            DropForeignKey("dbo.Receipts", "InvoiceIV", "dbo.Invoices");
            DropForeignKey("dbo.Receipts", "CremationTransactionAF", "dbo.CremationTransactions");
            DropForeignKey("dbo.Receipts", "ColumbariumTransactionAF", "dbo.ColumbariumTransactions");
            DropForeignKey("dbo.Receipts", "CemeteryTransactionAF", "dbo.CemeteryTransactions");
            DropForeignKey("dbo.Receipts", "AncestralTabletTransactionAF", "dbo.AncestralTabletTransactions");
            DropForeignKey("dbo.MiscellaneousTransactions", "MiscellaneousItemId", "dbo.MiscellaneousItems");
            DropForeignKey("dbo.MiscellaneousItems", "SubProductServiceId", "dbo.SubProductServices");
            DropForeignKey("dbo.MiscellaneousItems", "MiscellaneousId", "dbo.Miscellaneous");
            DropForeignKey("dbo.Miscellaneous", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.MiscellaneousTransactions", "CemeteryLandscapeCompanyId", "dbo.CemeteryLandscapeCompanies");
            DropForeignKey("dbo.MiscellaneousTransactions", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Invoices", "CremationTransactionAF", "dbo.CremationTransactions");
            DropForeignKey("dbo.Invoices", "ColumbariumTransactionAF", "dbo.ColumbariumTransactions");
            DropForeignKey("dbo.Invoices", "CemeteryTransactionAF", "dbo.CemeteryTransactions");
            DropForeignKey("dbo.Invoices", "AncestralTabletTransactionAF", "dbo.AncestralTabletTransactions");
            DropForeignKey("dbo.CremationTransactions", "FuneralCompanyId", "dbo.FuneralCompanies");
            DropForeignKey("dbo.CremationTransactions", "DeceasedId", "dbo.Deceaseds");
            DropForeignKey("dbo.CremationTransactions", "CremationItemId", "dbo.CremationItems");
            DropForeignKey("dbo.CremationItems", "SubProductServiceId", "dbo.SubProductServices");
            DropForeignKey("dbo.CremationItems", "CremationId", "dbo.Cremations");
            DropForeignKey("dbo.Cremations", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.CremationTransactions", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ColumbariumTransactions", "Deceased2Id", "dbo.Deceaseds");
            DropForeignKey("dbo.ColumbariumTransactions", "Deceased1Id", "dbo.Deceaseds");
            DropForeignKey("dbo.ColumbariumTransactions", "ColumbariumItemId", "dbo.ColumbariumItems");
            DropForeignKey("dbo.ColumbariumTransactions", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ColumbariumTrackings", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Niches", "ColumbariumAreaId", "dbo.ColumbariumAreas");
            DropForeignKey("dbo.Niches", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ColumbariumAreas", "ColumbariumCentreId", "dbo.ColumbariumCentres");
            DropForeignKey("dbo.Plots", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.CemeteryTransactions", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.CemeteryTrackings", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ApplicantDeceaseds", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.AncestralTabletTrackings", "AncestralTabletTransactionAF", "dbo.AncestralTabletTransactions");
            DropForeignKey("dbo.AncestralTabletTrackings", "AncestralTabletId", "dbo.AncestralTablets");
            DropForeignKey("dbo.AncestralTablets", "AncestralTabletAreaId", "dbo.AncestralTabletAreas");
            DropForeignKey("dbo.AncestralTabletItems", "AncestralTabletAreaId", "dbo.AncestralTabletAreas");
            DropIndex("dbo.Urns", new[] { "SiteId" });
            DropIndex("dbo.UrnItems", new[] { "SubProductServiceId" });
            DropIndex("dbo.UrnItems", new[] { "UrnId" });
            DropIndex("dbo.UrnTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.UrnTransactions", new[] { "UrnItemId" });
            DropIndex("dbo.Spaces", new[] { "SiteId" });
            DropIndex("dbo.SpaceItems", new[] { "SubProductServiceId" });
            DropIndex("dbo.SpaceItems", new[] { "SpaceId" });
            DropIndex("dbo.SpaceTransactions", new[] { "DeceasedId" });
            DropIndex("dbo.SpaceTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.SpaceTransactions", new[] { "FuneralCompanyId" });
            DropIndex("dbo.SpaceTransactions", new[] { "SpaceItemId" });
            DropIndex("dbo.Receipts", new[] { "PaymentMethodId" });
            DropIndex("dbo.Receipts", new[] { "UrnTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "SpaceTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "ColumbariumTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "MiscellaneousTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "AncestralTabletTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "CremationTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "CemeteryTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "InvoiceIV" });
            DropIndex("dbo.Miscellaneous", new[] { "SiteId" });
            DropIndex("dbo.MiscellaneousItems", new[] { "SubProductServiceId" });
            DropIndex("dbo.MiscellaneousItems", new[] { "MiscellaneousId" });
            DropIndex("dbo.MiscellaneousTransactions", new[] { "CemeteryLandscapeCompanyId" });
            DropIndex("dbo.MiscellaneousTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.MiscellaneousTransactions", new[] { "MiscellaneousItemId" });
            DropIndex("dbo.Invoices", new[] { "UrnTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "SpaceTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "ColumbariumTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "MiscellaneousTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "AncestralTabletTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "CremationTransactionAF" });
            DropIndex("dbo.Invoices", new[] { "CemeteryTransactionAF" });
            DropIndex("dbo.Cremations", new[] { "SiteId" });
            DropIndex("dbo.CremationItems", new[] { "SubProductServiceId" });
            DropIndex("dbo.CremationItems", new[] { "CremationId" });
            DropIndex("dbo.CremationTransactions", new[] { "DeceasedId" });
            DropIndex("dbo.CremationTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.CremationTransactions", new[] { "FuneralCompanyId" });
            DropIndex("dbo.CremationTransactions", new[] { "CremationItemId" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "TransferredColumbariumTransactionAF" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "TransferredApplicantId" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "ShiftedColumbariumTransactionAF" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "TransferredFromApplicantId" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "ShiftedNicheId" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "Deceased2Id" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "Deceased1Id" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "FuneralCompanyId" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "NicheId" });
            DropIndex("dbo.ColumbariumTransactions", new[] { "ColumbariumItemId" });
            DropIndex("dbo.ColumbariumTrackings", new[] { "Deceased2Id" });
            DropIndex("dbo.ColumbariumTrackings", new[] { "Deceased1Id" });
            DropIndex("dbo.ColumbariumTrackings", new[] { "ApplicantId" });
            DropIndex("dbo.ColumbariumTrackings", new[] { "ColumbariumTransactionAF" });
            DropIndex("dbo.ColumbariumTrackings", new[] { "NicheId" });
            DropIndex("dbo.Niches", new[] { "ApplicantId" });
            DropIndex("dbo.Niches", new[] { "ColumbariumAreaId" });
            DropIndex("dbo.Niches", new[] { "NicheTypeId" });
            DropIndex("dbo.ColumbariumAreas", new[] { "ColumbariumCentreId" });
            DropIndex("dbo.ColumbariumCentres", new[] { "SiteId" });
            DropIndex("dbo.ColumbariumItems", new[] { "ColumbariumCentreId" });
            DropIndex("dbo.ColumbariumItems", new[] { "SubProductServiceId" });
            DropIndex("dbo.SubProductServices", new[] { "ProductId" });
            DropIndex("dbo.Catalogs", new[] { "SiteId" });
            DropIndex("dbo.Catalogs", new[] { "ProductId" });
            DropIndex("dbo.CemeteryAreas", new[] { "SiteId" });
            DropIndex("dbo.Plots", new[] { "ApplicantId" });
            DropIndex("dbo.Plots", new[] { "CemeteryAreaId" });
            DropIndex("dbo.Plots", new[] { "PlotTypeId" });
            DropIndex("dbo.CemeteryItems", new[] { "SubProductServiceId" });
            DropIndex("dbo.CemeteryItems", new[] { "PlotId" });
            DropIndex("dbo.CemeteryTransactions", new[] { "TransferredCemeteryTransactionAF" });
            DropIndex("dbo.CemeteryTransactions", new[] { "TransferredApplicantId" });
            DropIndex("dbo.CemeteryTransactions", new[] { "ClearedApplicantId" });
            DropIndex("dbo.CemeteryTransactions", new[] { "Deceased3Id" });
            DropIndex("dbo.CemeteryTransactions", new[] { "Deceased2Id" });
            DropIndex("dbo.CemeteryTransactions", new[] { "Deceased1Id" });
            DropIndex("dbo.CemeteryTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.CemeteryTransactions", new[] { "FuneralCompanyId" });
            DropIndex("dbo.CemeteryTransactions", new[] { "FengShuiMasterId" });
            DropIndex("dbo.CemeteryTransactions", new[] { "PlotId" });
            DropIndex("dbo.CemeteryTransactions", new[] { "CemeteryItemId" });
            DropIndex("dbo.CemeteryTrackings", new[] { "Deceased3Id" });
            DropIndex("dbo.CemeteryTrackings", new[] { "Deceased2Id" });
            DropIndex("dbo.CemeteryTrackings", new[] { "Deceased1Id" });
            DropIndex("dbo.CemeteryTrackings", new[] { "ApplicantId" });
            DropIndex("dbo.CemeteryTrackings", new[] { "CemeteryTransactionAF" });
            DropIndex("dbo.CemeteryTrackings", new[] { "PlotId" });
            DropIndex("dbo.Deceaseds", new[] { "AncestralTabletId" });
            DropIndex("dbo.Deceaseds", new[] { "PlotId" });
            DropIndex("dbo.Deceaseds", new[] { "NicheId" });
            DropIndex("dbo.Deceaseds", new[] { "ReligionTypeId" });
            DropIndex("dbo.Deceaseds", new[] { "MaritalTypeId" });
            DropIndex("dbo.Deceaseds", new[] { "NationalityTypeId" });
            DropIndex("dbo.Deceaseds", new[] { "GenderTypeId" });
            DropIndex("dbo.ApplicantDeceaseds", new[] { "RelationshipTypeId" });
            DropIndex("dbo.ApplicantDeceaseds", new[] { "DeceasedId" });
            DropIndex("dbo.ApplicantDeceaseds", new[] { "ApplicantId" });
            DropIndex("dbo.AncestralTabletTrackings", new[] { "DeceasedId" });
            DropIndex("dbo.AncestralTabletTrackings", new[] { "ApplicantId" });
            DropIndex("dbo.AncestralTabletTrackings", new[] { "AncestralTabletTransactionAF" });
            DropIndex("dbo.AncestralTabletTrackings", new[] { "AncestralTabletId" });
            DropIndex("dbo.AncestralTablets", new[] { "AncestralTabletAreaId" });
            DropIndex("dbo.AncestralTablets", new[] { "ApplicantId" });
            DropIndex("dbo.AncestralTabletTransactions", new[] { "ShiftedAncestralTabletTransactionAF" });
            DropIndex("dbo.AncestralTabletTransactions", new[] { "ShiftedAncestralTabletId" });
            DropIndex("dbo.AncestralTabletTransactions", new[] { "DeceasedId" });
            DropIndex("dbo.AncestralTabletTransactions", new[] { "ApplicantId" });
            DropIndex("dbo.AncestralTabletTransactions", new[] { "AncestralTabletId" });
            DropIndex("dbo.AncestralTabletTransactions", new[] { "AncestralTabletItemId" });
            DropIndex("dbo.AncestralTabletItems", new[] { "AncestralTabletAreaId" });
            DropIndex("dbo.AncestralTabletItems", new[] { "SubProductServiceId" });
            DropIndex("dbo.AncestralTabletAreas", new[] { "SiteId" });
            DropTable("dbo.UrnNumbers");
            DropTable("dbo.SpaceNumbers");
            DropTable("dbo.MiscellaneousNumbers");
            DropTable("dbo.CremationNumbers");
            DropTable("dbo.ColumbariumNumbers");
            DropTable("dbo.CemeteryNumbers");
            DropTable("dbo.AncestralTabletNumbers");
            DropTable("dbo.RelationshipTypes");
            DropTable("dbo.ReligionTypes");
            DropTable("dbo.NationalityTypes");
            DropTable("dbo.MaritalTypes");
            DropTable("dbo.GenderTypes");
            DropTable("dbo.FengShuiMasters");
            DropTable("dbo.PlotTypes");
            DropTable("dbo.NicheTypes");
            DropTable("dbo.Urns");
            DropTable("dbo.UrnItems");
            DropTable("dbo.UrnTransactions");
            DropTable("dbo.Spaces");
            DropTable("dbo.SpaceItems");
            DropTable("dbo.SpaceTransactions");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.Receipts");
            DropTable("dbo.Miscellaneous");
            DropTable("dbo.MiscellaneousItems");
            DropTable("dbo.CemeteryLandscapeCompanies");
            DropTable("dbo.MiscellaneousTransactions");
            DropTable("dbo.Invoices");
            DropTable("dbo.Cremations");
            DropTable("dbo.CremationItems");
            DropTable("dbo.CremationTransactions");
            DropTable("dbo.FuneralCompanies");
            DropTable("dbo.ColumbariumTransactions");
            DropTable("dbo.ColumbariumTrackings");
            DropTable("dbo.Niches");
            DropTable("dbo.ColumbariumAreas");
            DropTable("dbo.ColumbariumCentres");
            DropTable("dbo.ColumbariumItems");
            DropTable("dbo.SubProductServices");
            DropTable("dbo.Products");
            DropTable("dbo.Catalogs");
            DropTable("dbo.Sites");
            DropTable("dbo.CemeteryAreas");
            DropTable("dbo.Plots");
            DropTable("dbo.CemeteryItems");
            DropTable("dbo.CemeteryTransactions");
            DropTable("dbo.CemeteryTrackings");
            DropTable("dbo.Deceaseds");
            DropTable("dbo.ApplicantDeceaseds");
            DropTable("dbo.Applicants");
            DropTable("dbo.AncestralTabletTrackings");
            DropTable("dbo.AncestralTablets");
            DropTable("dbo.AncestralTabletTransactions");
            DropTable("dbo.AncestralTabletItems");
            DropTable("dbo.AncestralTabletAreas");
            DropTable("dbo.AccessControls");
        }
    }
}
