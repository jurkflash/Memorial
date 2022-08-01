namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveRelationshipTypeIdFromAncestorsTableToAncestorTransactionsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ancestors", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropIndex("dbo.Ancestors", new[] { "RelationshipTypeId" });
            AddColumn("dbo.AncestorTransactions", "RelationshipTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.AncestorTransactions", "RelationshipTypeId");
            AddForeignKey("dbo.AncestorTransactions", "RelationshipTypeId", "dbo.RelationshipTypes", "Id");
            DropColumn("dbo.Ancestors", "RelationshipTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ancestors", "RelationshipTypeId", c => c.Byte());
            DropForeignKey("dbo.AncestorTransactions", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropIndex("dbo.AncestorTransactions", new[] { "RelationshipTypeId" });
            DropColumn("dbo.AncestorTransactions", "RelationshipTypeId");
            CreateIndex("dbo.Ancestors", "RelationshipTypeId");
            AddForeignKey("dbo.Ancestors", "RelationshipTypeId", "dbo.RelationshipTypes", "Id");
        }
    }
}
