namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToPaymentMethodsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO PaymentMethods(Id,Name,RequireRemark) VALUES (1,'Cash',0)");
            Sql("INSERT INTO PaymentMethods(Id,Name,RequireRemark) VALUES (2,'Credit Card',1)");
            Sql("INSERT INTO PaymentMethods(Id,Name,RequireRemark) VALUES (3,'Online Transfer',1)");
            Sql("INSERT INTO PaymentMethods(Id,Name,RequireRemark) VALUES (4,'Cheque',1)");
        }
        
        public override void Down()
        {
        }
    }
}
