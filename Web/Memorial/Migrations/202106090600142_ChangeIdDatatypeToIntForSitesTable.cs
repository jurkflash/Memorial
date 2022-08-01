namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIdDatatypeToIntForSitesTable : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE [dbo].[ColumbariumCentres] DROP CONSTRAINT [FK_dbo.QuadrangleCentres_dbo.Sites_SiteId]");
            Sql("ALTER TABLE [dbo].[CemeteryAreas] DROP CONSTRAINT [FK_dbo.PlotAreas_dbo.Sites_SiteId]");
            Sql("ALTER TABLE [dbo].[AncestralTabletAreas] DROP CONSTRAINT [FK_dbo.AncestorAreas_dbo.Sites_SiteId]");

            DropForeignKey("dbo.Cremations", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Miscellaneous", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Spaces", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Urns", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.ColumbariumCentres", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Catalogs", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.CemeteryAreas", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.AncestralTabletAreas", "SiteId", "dbo.Sites");
            DropIndex("dbo.AncestralTabletAreas", new[] { "SiteId" });
            DropIndex("dbo.CemeteryAreas", new[] { "SiteId" });
            DropIndex("dbo.Catalogs", new[] { "SiteId" });
            DropIndex("dbo.ColumbariumCentres", new[] { "SiteId" });
            DropIndex("dbo.Cremations", new[] { "SiteId" });
            DropIndex("dbo.Miscellaneous", new[] { "SiteId" });
            DropIndex("dbo.Spaces", new[] { "SiteId" });
            DropIndex("dbo.Urns", new[] { "SiteId" });
            DropPrimaryKey("dbo.Sites");
            AlterColumn("dbo.AncestralTabletAreas", "SiteId", c => c.Int(nullable: false));
            AlterColumn("dbo.CemeteryAreas", "SiteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sites", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Catalogs", "SiteId", c => c.Int(nullable: false));
            AlterColumn("dbo.ColumbariumCentres", "SiteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cremations", "SiteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Miscellaneous", "SiteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Spaces", "SiteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Urns", "SiteId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Sites", "Id");
            CreateIndex("dbo.AncestralTabletAreas", "SiteId");
            CreateIndex("dbo.CemeteryAreas", "SiteId");
            CreateIndex("dbo.Catalogs", "SiteId");
            CreateIndex("dbo.ColumbariumCentres", "SiteId");
            CreateIndex("dbo.Cremations", "SiteId");
            CreateIndex("dbo.Miscellaneous", "SiteId");
            CreateIndex("dbo.Spaces", "SiteId");
            CreateIndex("dbo.Urns", "SiteId");
            AddForeignKey("dbo.Cremations", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Miscellaneous", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Spaces", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Urns", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.ColumbariumCentres", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Catalogs", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.CemeteryAreas", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.AncestralTabletAreas", "SiteId", "dbo.Sites", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AncestralTabletAreas", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.CemeteryAreas", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Catalogs", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.ColumbariumCentres", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Urns", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Spaces", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Miscellaneous", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Cremations", "SiteId", "dbo.Sites");
            DropIndex("dbo.Urns", new[] { "SiteId" });
            DropIndex("dbo.Spaces", new[] { "SiteId" });
            DropIndex("dbo.Miscellaneous", new[] { "SiteId" });
            DropIndex("dbo.Cremations", new[] { "SiteId" });
            DropIndex("dbo.ColumbariumCentres", new[] { "SiteId" });
            DropIndex("dbo.Catalogs", new[] { "SiteId" });
            DropIndex("dbo.CemeteryAreas", new[] { "SiteId" });
            DropIndex("dbo.AncestralTabletAreas", new[] { "SiteId" });
            DropPrimaryKey("dbo.Sites");
            AlterColumn("dbo.Urns", "SiteId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Spaces", "SiteId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Miscellaneous", "SiteId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Cremations", "SiteId", c => c.Byte(nullable: false));
            AlterColumn("dbo.ColumbariumCentres", "SiteId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Catalogs", "SiteId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Sites", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.CemeteryAreas", "SiteId", c => c.Byte(nullable: false));
            AlterColumn("dbo.AncestralTabletAreas", "SiteId", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Sites", "Id");
            CreateIndex("dbo.Urns", "SiteId");
            CreateIndex("dbo.Spaces", "SiteId");
            CreateIndex("dbo.Miscellaneous", "SiteId");
            CreateIndex("dbo.Cremations", "SiteId");
            CreateIndex("dbo.ColumbariumCentres", "SiteId");
            CreateIndex("dbo.Catalogs", "SiteId");
            CreateIndex("dbo.CemeteryAreas", "SiteId");
            CreateIndex("dbo.AncestralTabletAreas", "SiteId");
            AddForeignKey("dbo.AncestralTabletAreas", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.CemeteryAreas", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Catalogs", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.ColumbariumCentres", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Urns", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Spaces", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Miscellaneous", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Cremations", "SiteId", "dbo.Sites", "Id");
        }
    }
}
