namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (1,N'樂捐',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (2,N'賠償費',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (3,N'租借冷風機',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (4,N'提早放靈屋（舖貼）',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (5,N'組椅子',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (6,N'維修墳墓',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (7,N'加骨灰位號碼',3); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
        }
        
        public override void Down()
        {
            Sql("TRUNCATE TABLE Miscellaneous");
        }
    }
}
