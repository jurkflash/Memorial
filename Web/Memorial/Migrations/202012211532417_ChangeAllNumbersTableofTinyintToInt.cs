namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAllNumbersTableofTinyintToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AncestorNumbers", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.AncestorNumbers", "AF", c => c.Int(nullable: false));
            AlterColumn("dbo.AncestorNumbers", "PO", c => c.Int(nullable: false));
            AlterColumn("dbo.AncestorNumbers", "IV", c => c.Int(nullable: false));
            AlterColumn("dbo.AncestorNumbers", "RE", c => c.Int(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "AF", c => c.Int(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "PO", c => c.Int(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "IV", c => c.Int(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "RE", c => c.Int(nullable: false));
            AlterColumn("dbo.PlotNumbers", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.PlotNumbers", "AF", c => c.Int(nullable: false));
            AlterColumn("dbo.PlotNumbers", "PO", c => c.Int(nullable: false));
            AlterColumn("dbo.PlotNumbers", "IV", c => c.Int(nullable: false));
            AlterColumn("dbo.PlotNumbers", "RE", c => c.Int(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "AF", c => c.Int(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "PO", c => c.Int(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "IV", c => c.Int(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "RE", c => c.Int(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "AF", c => c.Int(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "PO", c => c.Int(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "IV", c => c.Int(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "RE", c => c.Int(nullable: false));
            AlterColumn("dbo.UrnNumbers", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.UrnNumbers", "AF", c => c.Int(nullable: false));
            AlterColumn("dbo.UrnNumbers", "PO", c => c.Int(nullable: false));
            AlterColumn("dbo.UrnNumbers", "IV", c => c.Int(nullable: false));
            AlterColumn("dbo.UrnNumbers", "RE", c => c.Int(nullable: false));
            AlterColumn("dbo.CremationNumbers", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.CremationNumbers", "AF", c => c.Int(nullable: false));
            AlterColumn("dbo.CremationNumbers", "PO", c => c.Int(nullable: false));
            AlterColumn("dbo.CremationNumbers", "IV", c => c.Int(nullable: false));
            AlterColumn("dbo.CremationNumbers", "RE", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CremationNumbers", "RE", c => c.Byte(nullable: false));
            AlterColumn("dbo.CremationNumbers", "IV", c => c.Byte(nullable: false));
            AlterColumn("dbo.CremationNumbers", "PO", c => c.Byte(nullable: false));
            AlterColumn("dbo.CremationNumbers", "AF", c => c.Byte(nullable: false));
            AlterColumn("dbo.CremationNumbers", "Year", c => c.Byte(nullable: false));
            AlterColumn("dbo.UrnNumbers", "RE", c => c.Byte(nullable: false));
            AlterColumn("dbo.UrnNumbers", "IV", c => c.Byte(nullable: false));
            AlterColumn("dbo.UrnNumbers", "PO", c => c.Byte(nullable: false));
            AlterColumn("dbo.UrnNumbers", "AF", c => c.Byte(nullable: false));
            AlterColumn("dbo.UrnNumbers", "Year", c => c.Byte(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "RE", c => c.Byte(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "IV", c => c.Byte(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "PO", c => c.Byte(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "AF", c => c.Byte(nullable: false));
            AlterColumn("dbo.SpaceNumbers", "Year", c => c.Byte(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "RE", c => c.Byte(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "IV", c => c.Byte(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "PO", c => c.Byte(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "AF", c => c.Byte(nullable: false));
            AlterColumn("dbo.QuadrangleNumbers", "Year", c => c.Byte(nullable: false));
            AlterColumn("dbo.PlotNumbers", "RE", c => c.Byte(nullable: false));
            AlterColumn("dbo.PlotNumbers", "IV", c => c.Byte(nullable: false));
            AlterColumn("dbo.PlotNumbers", "PO", c => c.Byte(nullable: false));
            AlterColumn("dbo.PlotNumbers", "AF", c => c.Byte(nullable: false));
            AlterColumn("dbo.PlotNumbers", "Year", c => c.Byte(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "RE", c => c.Byte(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "IV", c => c.Byte(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "PO", c => c.Byte(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "AF", c => c.Byte(nullable: false));
            AlterColumn("dbo.MiscellaneousNumbers", "Year", c => c.Byte(nullable: false));
            AlterColumn("dbo.AncestorNumbers", "RE", c => c.Byte(nullable: false));
            AlterColumn("dbo.AncestorNumbers", "IV", c => c.Byte(nullable: false));
            AlterColumn("dbo.AncestorNumbers", "PO", c => c.Byte(nullable: false));
            AlterColumn("dbo.AncestorNumbers", "AF", c => c.Byte(nullable: false));
            AlterColumn("dbo.AncestorNumbers", "Year", c => c.Byte(nullable: false));
        }
    }
}
