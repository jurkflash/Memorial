namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToSpaceItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (1,'Booking',750,'False','False',2,'SBG01',1); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (2,'Booking',750,'False','False',2,'SBG02',2); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (3,'Booking',750,'False','False',2,'SBG03',3); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (4,'Booking',750,'False','False',2,'SBG04',4); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (5,'Booking',0,'False','False',2,'SBG05',5); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (6,'Booking',650,'False','False',2,'SBG06',6); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (7,'Booking',750,'False','False',2,'SBG07',7); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (8,'Booking',750,'False','False',2,'SBG08',8); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (9,'Booking',750,'False','False',2,'SBG09',9); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (10,'Booking',750,'False','False',2,'SBG10',10); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (11,'Booking',750,'False','False',2,'SBG11',11); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (12,'Booking',1500,'True','False',2,'SGDTA',12); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (13,'Booking',1500,'True','False',2,'SGDTB',13); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (14,'Booking',1000,'True','False',2,'SGDTC',14); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (15,'Booking',3000,'True','False',2,'SSNBT',15); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (16,'Booking',3000,'True','False',2,'SHALL',16); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (17,'Booking',1500,'False','False',2,'SJSTN',17); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (18,'Booking',1500,'False','False',2,'SJDTN',18); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (19,'Booking',300,'False','True',0,'SJKDS',19); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (20,'Booking',300,'False','True',0,'SDKDS',20); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (21,'Free',0,'False','False',2,'SBG01',1); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (22,'Free',0,'False','False',2,'SBG02',2); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (23,'Free',0,'False','False',2,'SBG03',3); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (24,'Free',0,'False','False',2,'SBG04',4); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (25,'Free',0,'False','False',2,'SBG05',5); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (26,'Free',0,'False','False',2,'SBG06',6); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (27,'Free',0,'False','False',2,'SBG07',7); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (28,'Free',0,'False','False',2,'SBG08',8); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (29,'Free',0,'False','False',2,'SBG09',9); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (30,'Free',0,'False','False',2,'SBG10',10); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (31,'Free',0,'False','False',2,'SBG11',11); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (32,'Chair',0,'False','False',0,'SSBTC',15); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (33,'Electric',0,'False','False',0,'SSBTE',15); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (34,'House',0,'False','False',0,'SSBTH',15); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (35,'Chair',0,'False','False',0,'SHALC',16); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (36,'Electric',0,'False','False',0,'SHALE',16); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
            Sql("SET IDENTITY_INSERT SpaceItems ON; " +
                "INSERT INTO SpaceItems(Id,Name,Price,AllowDeposit,AllowDoubleBook,ToleranceHour,Code,SpaceId) VALUES (37,'House',0,'False','False',0,'SHALH',16); " +
                "SET IDENTITY_INSERT SpaceItems OFF;");
        }

        public override void Down()
        {
            Sql("DELETE FROM SpaceItems; DBCC CHECKIDENT('SpaceItems', RESEED, 0); ");
        }
    }
}
