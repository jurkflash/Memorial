namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAFColumnFromInvoicesTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Invoices", "AF");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "AF", c => c.String());
        }
    }
}
