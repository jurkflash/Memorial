namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAncestorTransactionAFToAncestorsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AncestorTransactions", "AncestorId", "dbo.Ancestors");
            AddForeignKey("dbo.AncestorTransactions", "AncestorId", "dbo.Ancestors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AncestorTransactions", "AncestorId", "dbo.Ancestors");
            AddForeignKey("dbo.AncestorTransactions", "AncestorId", "dbo.Ancestors", "Id", cascadeDelete: true);
        }
    }
}
