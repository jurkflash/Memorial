namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNiche_IdColumnFromColumbariumTrackingsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ColumbariumTrackings", "Niche_Id", "dbo.Niches");
            DropIndex("dbo.ColumbariumTrackings", new[] { "Niche_Id" });
            DropColumn("dbo.ColumbariumTrackings", "Niche_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ColumbariumTrackings", "Niche_Id", c => c.Int());
            CreateIndex("dbo.ColumbariumTrackings", "Niche_Id");
            AddForeignKey("dbo.ColumbariumTrackings", "Niche_Id", "dbo.Niches", "Id");
        }
    }
}
