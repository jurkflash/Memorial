namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataAndUpdateDataForSpacesTable : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE Spaces");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (1,N'殡仪馆一号',750,'False',2,2,'SBG01'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (2,N'殡仪馆二号',750,'False',2,2,'SBG02'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (3,N'殡仪馆三号',750,'False',2,2,'SBG03'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (4,N'殡仪馆四号',750,'False',2,2,'SBG04'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,Remark,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (5,N'殡仪馆五号',N'无冷气',0,'False',2,2,'SBG05'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (6,N'殡仪馆六号',650,'False',2,2,'SBG06'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (7,N'殡仪馆七号',750,'False',2,2,'SBG07'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (8,N'殡仪馆八号',750,'False',2,2,'SBG08'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (9,N'殡仪馆九号',750,'False',2,2,'SBG09'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (10,N'殡仪馆十号',750,'False',2,2,'SBG10'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (11,N'殡仪馆十一号',750,'False',2,2,'SBG11'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (12,N'功德壇 A',1500,'False',2,2,'SGDTA'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (13,N'功德壇 B',1500,'False',2,2,'SGDTB'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (14,N'功德壇 C',1000,'False',2,2,'SGDTC'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (15,N'三寶壇',3000,'False',2,2,'SSNBT'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (16,N'大禮堂',3000,'False',2,2,'SHALL'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (17,N'積善廳',1500,'False',2,2,'SJSTN'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (18,N'積德廳',1500,'False',2,2,'SJDTN'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (19,N'空地烧',300,'True',0,2,'SJKDS'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId,Code) VALUES (20,N'空地烧',300,'True',0,3,'SDKDS'); " +
                "SET IDENTITY_INSERT Spaces OFF;");
        }

        public override void Down()
        {
            Sql("TRUNCATE TABLE Spaces");
        }
    }
}
