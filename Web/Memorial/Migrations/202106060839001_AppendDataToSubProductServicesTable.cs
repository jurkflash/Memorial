namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppendDataToSubProductServicesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'預訂 Booking','',500,'SBOOK','Bookings',1,6)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'免費 Free','',0,'SFREE','Free',0,6)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'租椅子 Chair','',0,'SPZYZ','Chairs',1,6)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'電費 Electric','',0,'SPCDF','Electric',1,6)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'提早放靈屋 House','',0,'SPFLW','Houses',1,6)");
        }
        
        public override void Down()
        {
        }
    }
}
