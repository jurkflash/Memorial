namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequireRemarkToPaymentMethodsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentMethods", "RequireRemark", c => c.Boolean(nullable: false));
            Sql("UPDATE PaymentMethods SET RequireRemark='True' WHERE Name<>'Cash'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentMethods", "RequireRemark");
        }
    }
}
