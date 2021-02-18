namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToSpacesTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (1,N'殡仪馆一号',750,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (2,N'殡仪馆二号',750,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (3,N'殡仪馆三号',750,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (4,N'殡仪馆四号',750,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,Remark,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (5,N'殡仪馆五号',N'无冷气',0,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (6,N'殡仪馆六号',650,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (7,N'殡仪馆七号',750,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (8,N'殡仪馆八号',750,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (9,N'殡仪馆九号',750,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (10,N'殡仪馆十号',750,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (11,N'殡仪馆十一号',750,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (12,N'功德壇 A',1500,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (13,N'功德壇 B',1500,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (14,N'功德壇 C',1000,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (15,N'三寶壇',3000,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (16,N'大禮堂',3000,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (17,N'積善廳',1500,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (18,N'積德廳',1500,'False',2,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (19,N'空地烧',300,'True',0,2); " +
                "SET IDENTITY_INSERT Spaces OFF;");
            Sql("SET IDENTITY_INSERT Spaces ON; " +
                "INSERT INTO Spaces(Id,Name,PricePerDay,AllowDoubleBook,ToleranceHour,SiteId) VALUES (20,N'空地烧',300,'True',0,3); " +
                "SET IDENTITY_INSERT Spaces OFF;");
        }

        public override void Down()
        {
            Sql("TRUNCATE TABLE Spaces");
        }
    }
}
