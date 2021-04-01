namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeItemIdToItemCodeForAllNumbersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AncestorNumbers", "AncestorItemId", "dbo.AncestorItems");
            DropForeignKey("dbo.MiscellaneousNumbers", "MiscellaneousItemId", "dbo.MiscellaneousItems");
            DropForeignKey("dbo.PlotNumbers", "PlotItemId", "dbo.PlotItems");
            DropForeignKey("dbo.SpaceNumbers", "SpaceItemId", "dbo.SpaceItems");
            DropForeignKey("dbo.QuadrangleNumbers", "QuadrangleItemId", "dbo.QuadrangleItems");
            DropForeignKey("dbo.UrnNumbers", "UrnItemId", "dbo.UrnItems");
            DropForeignKey("dbo.CremationNumbers", "CremationItemId", "dbo.CremationItems");
            DropIndex("dbo.AncestorNumbers", new[] { "AncestorItemId" });
            DropIndex("dbo.MiscellaneousNumbers", new[] { "MiscellaneousItemId" });
            DropIndex("dbo.PlotNumbers", new[] { "PlotItemId" });
            DropIndex("dbo.SpaceNumbers", new[] { "SpaceItemId" });
            DropIndex("dbo.QuadrangleNumbers", new[] { "QuadrangleItemId" });
            DropIndex("dbo.UrnNumbers", new[] { "UrnItemId" });
            DropIndex("dbo.CremationNumbers", new[] { "CremationItemId" });
            AddColumn("dbo.AncestorNumbers", "ItemCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.MiscellaneousNumbers", "ItemCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.PlotNumbers", "ItemCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.SpaceNumbers", "ItemCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.QuadrangleNumbers", "ItemCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.UrnNumbers", "ItemCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.CremationNumbers", "ItemCode", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.AncestorNumbers", "AncestorItemId");
            DropColumn("dbo.MiscellaneousNumbers", "MiscellaneousItemId");
            DropColumn("dbo.PlotNumbers", "PlotItemId");
            DropColumn("dbo.SpaceNumbers", "SpaceItemId");
            DropColumn("dbo.QuadrangleNumbers", "QuadrangleItemId");
            DropColumn("dbo.UrnNumbers", "UrnItemId");
            DropColumn("dbo.CremationNumbers", "CremationItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CremationNumbers", "CremationItemId", c => c.Int(nullable: false));
            AddColumn("dbo.UrnNumbers", "UrnItemId", c => c.Int(nullable: false));
            AddColumn("dbo.QuadrangleNumbers", "QuadrangleItemId", c => c.Int(nullable: false));
            AddColumn("dbo.SpaceNumbers", "SpaceItemId", c => c.Int(nullable: false));
            AddColumn("dbo.PlotNumbers", "PlotItemId", c => c.Int(nullable: false));
            AddColumn("dbo.MiscellaneousNumbers", "MiscellaneousItemId", c => c.Int(nullable: false));
            AddColumn("dbo.AncestorNumbers", "AncestorItemId", c => c.Int(nullable: false));
            DropColumn("dbo.CremationNumbers", "ItemCode");
            DropColumn("dbo.UrnNumbers", "ItemCode");
            DropColumn("dbo.QuadrangleNumbers", "ItemCode");
            DropColumn("dbo.SpaceNumbers", "ItemCode");
            DropColumn("dbo.PlotNumbers", "ItemCode");
            DropColumn("dbo.MiscellaneousNumbers", "ItemCode");
            DropColumn("dbo.AncestorNumbers", "ItemCode");
            CreateIndex("dbo.CremationNumbers", "CremationItemId");
            CreateIndex("dbo.UrnNumbers", "UrnItemId");
            CreateIndex("dbo.QuadrangleNumbers", "QuadrangleItemId");
            CreateIndex("dbo.SpaceNumbers", "SpaceItemId");
            CreateIndex("dbo.PlotNumbers", "PlotItemId");
            CreateIndex("dbo.MiscellaneousNumbers", "MiscellaneousItemId");
            CreateIndex("dbo.AncestorNumbers", "AncestorItemId");
            AddForeignKey("dbo.CremationNumbers", "CremationItemId", "dbo.CremationItems", "Id");
            AddForeignKey("dbo.UrnNumbers", "UrnItemId", "dbo.UrnItems", "Id");
            AddForeignKey("dbo.QuadrangleNumbers", "QuadrangleItemId", "dbo.QuadrangleItems", "Id");
            AddForeignKey("dbo.SpaceNumbers", "SpaceItemId", "dbo.SpaceItems", "Id");
            AddForeignKey("dbo.PlotNumbers", "PlotItemId", "dbo.PlotItems", "Id");
            AddForeignKey("dbo.MiscellaneousNumbers", "MiscellaneousItemId", "dbo.MiscellaneousItems", "Id");
            AddForeignKey("dbo.AncestorNumbers", "AncestorItemId", "dbo.AncestorItems", "Id");
        }
    }
}
