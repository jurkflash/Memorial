namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShiftedQuadrangleIdToQuadrangleTrackingsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleTrackings", "ShiftedQuadrangleId", c => c.Int());
            CreateIndex("dbo.QuadrangleTrackings", "ShiftedQuadrangleId");
            AddForeignKey("dbo.QuadrangleTrackings", "ShiftedQuadrangleId", "dbo.Quadrangles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuadrangleTrackings", "ShiftedQuadrangleId", "dbo.Quadrangles");
            DropIndex("dbo.QuadrangleTrackings", new[] { "ShiftedQuadrangleId" });
            DropColumn("dbo.QuadrangleTrackings", "ShiftedQuadrangleId");
        }
    }
}
