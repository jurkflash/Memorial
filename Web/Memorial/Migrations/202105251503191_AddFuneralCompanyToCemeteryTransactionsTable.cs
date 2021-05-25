namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFuneralCompanyToCemeteryTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CemeteryTransactions", "FuneralCompanyId", c => c.Int());
            CreateIndex("dbo.CemeteryTransactions", "FuneralCompanyId");
            AddForeignKey("dbo.CemeteryTransactions", "FuneralCompanyId", "dbo.FuneralCompanies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CemeteryTransactions", "FuneralCompanyId", "dbo.FuneralCompanies");
            DropIndex("dbo.CemeteryTransactions", new[] { "FuneralCompanyId" });
            DropColumn("dbo.CemeteryTransactions", "FuneralCompanyId");
        }
    }
}
