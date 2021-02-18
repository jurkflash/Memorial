namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuadranglesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quadrangles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 100),
                        PositionX = c.Byte(nullable: false),
                        PositionY = c.Byte(nullable: false),
                        Price = c.Single(nullable: false),
                        Maintenance = c.Single(nullable: false),
                        LifeTimeMaintenance = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                        QuadrangleTypeId = c.Byte(nullable: false),
                        QuadrangleAreaId = c.Int(nullable: false),
                        ApplicantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.QuadrangleAreas", t => t.QuadrangleAreaId)
                .ForeignKey("dbo.QuadrangleTypes", t => t.QuadrangleTypeId)
                .Index(t => t.QuadrangleTypeId)
                .Index(t => t.QuadrangleAreaId)
                .Index(t => t.ApplicantId);
            
            AddColumn("dbo.Deceaseds", "QuadrangleId", c => c.Int());
            CreateIndex("dbo.Deceaseds", "QuadrangleId");
            AddForeignKey("dbo.Deceaseds", "QuadrangleId", "dbo.Quadrangles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quadrangles", "QuadrangleTypeId", "dbo.QuadrangleTypes");
            DropForeignKey("dbo.Quadrangles", "QuadrangleAreaId", "dbo.QuadrangleAreas");
            DropForeignKey("dbo.Deceaseds", "QuadrangleId", "dbo.Quadrangles");
            DropForeignKey("dbo.Quadrangles", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Quadrangles", new[] { "ApplicantId" });
            DropIndex("dbo.Quadrangles", new[] { "QuadrangleAreaId" });
            DropIndex("dbo.Quadrangles", new[] { "QuadrangleTypeId" });
            DropIndex("dbo.Deceaseds", new[] { "QuadrangleId" });
            DropColumn("dbo.Deceaseds", "QuadrangleId");
            DropTable("dbo.Quadrangles");
        }
    }
}
