namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuadrangleTrackingParentToQuadrangleTrackingTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId", c => c.Int());
            CreateIndex("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId");
            AddForeignKey("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId", "dbo.QuadrangleTrackings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId", "dbo.QuadrangleTrackings");
            DropIndex("dbo.QuadrangleTrackings", new[] { "QuadrangleTrackingParentId" });
            DropColumn("dbo.QuadrangleTrackings", "QuadrangleTrackingParentId");
        }
    }
}
