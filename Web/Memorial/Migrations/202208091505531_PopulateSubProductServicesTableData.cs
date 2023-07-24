namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSubProductServicesTableData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'單 Order','',0,'ACSZP','Orders',1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'管理費 Maintenance','',20,'ACGLF','Maintenances',1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'移位 Shift','',0,'ACYWF','Shifts',1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'免費 Free','',0,'ACFRE','Free',0,2)");
            
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'單 Order','',0,'QRAF1','Orders',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'管理 Manage','',150,'QRGLF','Manage',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'照片 Photo','',150,'QRZPF','Photos',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'移位 Shift','',150,'QRYWF','Shifts',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'轉讓 Transfer','',200,'QRZRF','Transfers',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'免費 Free','',0,'QRFRE','Free',0,5)");
            
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'單 Order','',600,'CMHHD','Orders',1,3)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'免費 Free','',0,'CMFRE','Free',0,3)");
            
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'樂捐 Donation','',0,'MSLJF','Donations',0,7)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'賠償費 Compensate','',0,'MSLJF','Compensates',1,7)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'租借冷風機 Rent Air Cooler','',0,'MSLJF','RentAirCoolers',1,7)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'回饋 Reciprocate','',0,'MSLJF','Reciprocates',0,7)");
            
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'購買 Purchase','',0,'URNLJ','Purchases',1,4)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'購買 Purchase','',500,'SBOOK','Bookings',1,6)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'免費 Free','',0,'SFREE','Free',0,6)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'租椅子 Chair','',0,'SPZYZ','Chairs',1,6)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'電費 Electric','',0,'SPCDF','Electric',1,6)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'提早放靈屋 House','',0,'SPFLW','Houses',1,6)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'單 Order','',0,'CYSP1','Orders',1,1,1)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'單 Order','',0,'CYDP2','Orders',1,1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'單 Order','',0,'CYDP3','Orders',1,1,3)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'單 Order','',0,'CYFSD','Orders',1,1,4)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'轉讓 Transfer','',0,'CYFSF','FengShuiTransfers',1,1,4)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'附葬 Second Burial','',400,'SBFZF','SecondBurials',1,1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'附葬 Second Burial','',400,'SBFZF','SecondBurials',1,1,3)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'附葬 Second Burial','',400,'SBFZF','SecondBurials',1,1,4)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'拾金 Clearance','',0,'SJINF','Clearances',1,1,1)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'拾金 Clearance','',0,'SJINF','Clearances',1,1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'拾金 Clearance','',0,'SJINF','Clearances',1,1,3)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'拾金 Clearance','',0,'SJINF','Clearances',1,1,4)");
        }
        
        public override void Down()
        {
        }
    }
}
