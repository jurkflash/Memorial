namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataAndUpdateDataForMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE Miscellaneous");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
               "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (1,N'樂捐',2,'MSLJF'); " +
               "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (2,N'賠償費',2,'MSPCF'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (3,N'租借冷風機',2,'MSLFJ'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (4,N'維修墳墓',2,'MSRPP'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (5,N'加骨灰位號碼',3,'MSQCN'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (6,N'礼堂-組椅子',3,'MSLYZ'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (7,N'礼堂-電費',3,'MSLDF'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (8,N'礼堂-提早放靈屋（舖貼）',2,'MSLWB'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (9,N'三寶壇-組椅子',3,'MSSYZ'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (10,N'三寶壇-電費',3,'MSLDF'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId,Code) VALUES (11,N'三寶壇-提早放靈屋（舖貼）',2,'MSSLW'); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");

        }
        
        public override void Down()
        {
            Sql("TRUNCATE TABLE Miscellaneous");
        }
    }
}
