namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeceasedsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deceaseds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IC = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 255),
                        Name2 = c.String(),
                        Age = c.Byte(nullable: false),
                        Address = c.String(),
                        Remarks = c.String(),
                        GenderTypeId = c.Byte(nullable: false),
                        Province = c.String(),
                        NationalityTypeId = c.Byte(nullable: false),
                        MaritalTypeId = c.Byte(nullable: false),
                        ReligionTypeId = c.Byte(nullable: false),
                        RelationshipTypeId = c.Byte(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                        DeathPlace = c.String(),
                        DeathDate = c.DateTime(nullable: false),
                        DeathRegistrationCentre = c.String(),
                        DeathCertificate = c.String(),
                        BurialCertificate = c.String(),
                        ImportPermitNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.GenderTypes", t => t.GenderTypeId)
                .ForeignKey("dbo.MaritalTypes", t => t.MaritalTypeId)
                .ForeignKey("dbo.NationalityTypes", t => t.NationalityTypeId)
                .ForeignKey("dbo.RelationshipTypes", t => t.RelationshipTypeId)
                .ForeignKey("dbo.ReligionTypes", t => t.ReligionTypeId)
                .Index(t => t.GenderTypeId)
                .Index(t => t.NationalityTypeId)
                .Index(t => t.MaritalTypeId)
                .Index(t => t.ReligionTypeId)
                .Index(t => t.RelationshipTypeId)
                .Index(t => t.ApplicantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deceaseds", "ReligionTypeId", "dbo.ReligionTypes");
            DropForeignKey("dbo.Deceaseds", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropForeignKey("dbo.Deceaseds", "NationalityTypeId", "dbo.NationalityTypes");
            DropForeignKey("dbo.Deceaseds", "MaritalTypeId", "dbo.MaritalTypes");
            DropForeignKey("dbo.Deceaseds", "GenderTypeId", "dbo.GenderTypes");
            DropForeignKey("dbo.Deceaseds", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Deceaseds", new[] { "ApplicantId" });
            DropIndex("dbo.Deceaseds", new[] { "RelationshipTypeId" });
            DropIndex("dbo.Deceaseds", new[] { "ReligionTypeId" });
            DropIndex("dbo.Deceaseds", new[] { "MaritalTypeId" });
            DropIndex("dbo.Deceaseds", new[] { "NationalityTypeId" });
            DropIndex("dbo.Deceaseds", new[] { "GenderTypeId" });
            DropTable("dbo.Deceaseds");
        }
    }
}
