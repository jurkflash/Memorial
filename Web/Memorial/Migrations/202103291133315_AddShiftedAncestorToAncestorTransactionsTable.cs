namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShiftedAncestorToAncestorTransactionsTable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AncestorTransactions", "ShiftedAncestorId");
            AddForeignKey("dbo.AncestorTransactions", "ShiftedAncestorId", "dbo.Ancestors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AncestorTransactions", "ShiftedAncestorId", "dbo.Ancestors");
            DropIndex("dbo.AncestorTransactions", new[] { "ShiftedAncestorId" });
        }
    }
}
