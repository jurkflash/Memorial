namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuadrangleCentreIdColumnToQuadrangleItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleItems", "QuadrangleCentreId", c => c.Int(nullable: false, defaultValue: 2));
            CreateIndex("dbo.QuadrangleItems", "QuadrangleCentreId");
            AddForeignKey("dbo.QuadrangleItems", "QuadrangleCentreId", "dbo.QuadrangleCentres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleItems", "QuadrangleCentreId", "dbo.QuadrangleCentres");
            DropIndex("dbo.QuadrangleItems", new[] { "QuadrangleCentreId" });
            DropColumn("dbo.QuadrangleItems", "QuadrangleCentreId");
        }
    }
}
