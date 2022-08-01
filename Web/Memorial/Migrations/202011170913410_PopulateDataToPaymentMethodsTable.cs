namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToPaymentMethodsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO PaymentMethods(Id,Name) VALUES (1,'Cash')");
            Sql("INSERT INTO PaymentMethods(Id,Name) VALUES (2,'Credit Card')");
            Sql("INSERT INTO PaymentMethods(Id,Name) VALUES (3,'Online Transfer')");
            Sql("INSERT INTO PaymentMethods(Id,Name) VALUES (4,'Cheque')");
        }
        
        public override void Down()
        {
        }
    }
}
