namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSecondDeceasedToPlotTransactionsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PlotTransactions", name: "DeceasedId", newName: "Deceased1Id");
            RenameIndex(table: "dbo.PlotTransactions", name: "IX_DeceasedId", newName: "IX_Deceased1Id");
            AddColumn("dbo.PlotTransactions", "Deceased2Id", c => c.Int());
            AlterColumn("dbo.PlotTransactions", "Maintenance", c => c.Single());
            AlterColumn("dbo.PlotTransactions", "Wall", c => c.Single());
            AlterColumn("dbo.PlotTransactions", "Dig", c => c.Single());
            AlterColumn("dbo.PlotTransactions", "Brick", c => c.Single());
            CreateIndex("dbo.PlotTransactions", "Deceased2Id");
            AddForeignKey("dbo.PlotTransactions", "Deceased2Id", "dbo.Deceaseds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlotTransactions", "Deceased2Id", "dbo.Deceaseds");
            DropIndex("dbo.PlotTransactions", new[] { "Deceased2Id" });
            AlterColumn("dbo.PlotTransactions", "Brick", c => c.Single(nullable: false));
            AlterColumn("dbo.PlotTransactions", "Dig", c => c.Single(nullable: false));
            AlterColumn("dbo.PlotTransactions", "Wall", c => c.Single(nullable: false));
            AlterColumn("dbo.PlotTransactions", "Maintenance", c => c.Single(nullable: false));
            DropColumn("dbo.PlotTransactions", "Deceased2Id");
            RenameIndex(table: "dbo.PlotTransactions", name: "IX_Deceased1Id", newName: "IX_DeceasedId");
            RenameColumn(table: "dbo.PlotTransactions", name: "Deceased1Id", newName: "DeceasedId");
        }
    }
}
