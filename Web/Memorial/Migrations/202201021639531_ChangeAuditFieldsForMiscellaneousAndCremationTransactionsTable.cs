namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAuditFieldsForMiscellaneousAndCremationTransactionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CremationTransactions", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.CremationTransactions", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.CremationTransactions", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CremationTransactions", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.CremationTransactions", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.CremationTransactions", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.CremationTransactions", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.MiscellaneousTransactions", "ActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.MiscellaneousTransactions", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.MiscellaneousTransactions", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MiscellaneousTransactions", "ModifiedById", c => c.Int(nullable: false));
            AddColumn("dbo.MiscellaneousTransactions", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.MiscellaneousTransactions", "DeletedById", c => c.Int(nullable: false));
            AddColumn("dbo.MiscellaneousTransactions", "DeletedDate", c => c.DateTime());
            DropColumn("dbo.CremationTransactions", "CreateDate");
            DropColumn("dbo.CremationTransactions", "ModifyDate");
            DropColumn("dbo.CremationTransactions", "DeleteDate");
            DropColumn("dbo.MiscellaneousTransactions", "CreateDate");
            DropColumn("dbo.MiscellaneousTransactions", "ModifyDate");
            DropColumn("dbo.MiscellaneousTransactions", "DeleteDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MiscellaneousTransactions", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.MiscellaneousTransactions", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.MiscellaneousTransactions", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CremationTransactions", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.CremationTransactions", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.CremationTransactions", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.MiscellaneousTransactions", "DeletedDate");
            DropColumn("dbo.MiscellaneousTransactions", "DeletedById");
            DropColumn("dbo.MiscellaneousTransactions", "ModifiedDate");
            DropColumn("dbo.MiscellaneousTransactions", "ModifiedById");
            DropColumn("dbo.MiscellaneousTransactions", "CreatedDate");
            DropColumn("dbo.MiscellaneousTransactions", "CreatedById");
            DropColumn("dbo.MiscellaneousTransactions", "ActiveStatus");
            DropColumn("dbo.CremationTransactions", "DeletedDate");
            DropColumn("dbo.CremationTransactions", "DeletedById");
            DropColumn("dbo.CremationTransactions", "ModifiedDate");
            DropColumn("dbo.CremationTransactions", "ModifiedById");
            DropColumn("dbo.CremationTransactions", "CreatedDate");
            DropColumn("dbo.CremationTransactions", "CreatedById");
            DropColumn("dbo.CremationTransactions", "ActiveStatus");
        }
    }
}
