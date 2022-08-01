namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveQuadrangleTrackingParentFromQuadrangleTrackingsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId", "dbo.QuadrangleTrackings");
            DropIndex("dbo.QuadrangleTrackings", new[] { "QuadrangleTrackingParentId" });
            DropColumn("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId", c => c.Int());
            CreateIndex("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId");
            AddForeignKey("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId", "dbo.QuadrangleTrackings", "Id");
        }
    }
}
