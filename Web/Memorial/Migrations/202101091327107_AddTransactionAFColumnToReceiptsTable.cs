namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactionAFColumnToReceiptsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receipts", "PlotTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Receipts", "CremationTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Receipts", "AncestorTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Receipts", "MiscellaneousTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Receipts", "QuadrangleTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Receipts", "SpaceTransactionAF", c => c.String(maxLength: 50));
            AddColumn("dbo.Receipts", "UrnTransactionAF", c => c.String(maxLength: 50));
            CreateIndex("dbo.Receipts", "PlotTransactionAF");
            CreateIndex("dbo.Receipts", "CremationTransactionAF");
            CreateIndex("dbo.Receipts", "AncestorTransactionAF");
            CreateIndex("dbo.Receipts", "MiscellaneousTransactionAF");
            CreateIndex("dbo.Receipts", "QuadrangleTransactionAF");
            CreateIndex("dbo.Receipts", "SpaceTransactionAF");
            CreateIndex("dbo.Receipts", "UrnTransactionAF");
            AddForeignKey("dbo.Receipts", "AncestorTransactionAF", "dbo.AncestorTransactions", "AF");
            AddForeignKey("dbo.Receipts", "CremationTransactionAF", "dbo.CremationTransactions", "AF");
            AddForeignKey("dbo.Receipts", "MiscellaneousTransactionAF", "dbo.MiscellaneousTransactions", "AF");
            AddForeignKey("dbo.Receipts", "PlotTransactionAF", "dbo.PlotTransactions", "AF");
            AddForeignKey("dbo.Receipts", "QuadrangleTransactionAF", "dbo.QuadrangleTransactions", "AF");
            AddForeignKey("dbo.Receipts", "SpaceTransactionAF", "dbo.SpaceTransactions", "AF");
            AddForeignKey("dbo.Receipts", "UrnTransactionAF", "dbo.UrnTransactions", "AF");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receipts", "UrnTransactionAF", "dbo.UrnTransactions");
            DropForeignKey("dbo.Receipts", "SpaceTransactionAF", "dbo.SpaceTransactions");
            DropForeignKey("dbo.Receipts", "QuadrangleTransactionAF", "dbo.QuadrangleTransactions");
            DropForeignKey("dbo.Receipts", "PlotTransactionAF", "dbo.PlotTransactions");
            DropForeignKey("dbo.Receipts", "MiscellaneousTransactionAF", "dbo.MiscellaneousTransactions");
            DropForeignKey("dbo.Receipts", "CremationTransactionAF", "dbo.CremationTransactions");
            DropForeignKey("dbo.Receipts", "AncestorTransactionAF", "dbo.AncestorTransactions");
            DropIndex("dbo.Receipts", new[] { "UrnTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "SpaceTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "QuadrangleTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "MiscellaneousTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "AncestorTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "CremationTransactionAF" });
            DropIndex("dbo.Receipts", new[] { "PlotTransactionAF" });
            DropColumn("dbo.Receipts", "UrnTransactionAF");
            DropColumn("dbo.Receipts", "SpaceTransactionAF");
            DropColumn("dbo.Receipts", "QuadrangleTransactionAF");
            DropColumn("dbo.Receipts", "MiscellaneousTransactionAF");
            DropColumn("dbo.Receipts", "AncestorTransactionAF");
            DropColumn("dbo.Receipts", "CremationTransactionAF");
            DropColumn("dbo.Receipts", "PlotTransactionAF");
        }
    }
}
