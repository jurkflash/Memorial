namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasDeceasedAndRemoveRelationshipTypeForAncestorsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AncestorTransactions", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropIndex("dbo.AncestorTransactions", new[] { "RelationshipTypeId" });
            AddColumn("dbo.AncestorTransactions", "Maintenance", c => c.Single());
            AddColumn("dbo.Ancestors", "hasDeceased", c => c.Boolean(nullable: false));
            DropColumn("dbo.AncestorTransactions", "RelationshipTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AncestorTransactions", "RelationshipTypeId", c => c.Byte(nullable: false));
            DropColumn("dbo.Ancestors", "hasDeceased");
            DropColumn("dbo.AncestorTransactions", "Maintenance");
            CreateIndex("dbo.AncestorTransactions", "RelationshipTypeId");
            AddForeignKey("dbo.AncestorTransactions", "RelationshipTypeId", "dbo.RelationshipTypes", "Id");
        }
    }
}
