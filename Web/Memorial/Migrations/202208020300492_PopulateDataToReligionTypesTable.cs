namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToReligionTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ReligionTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (1,N'道教 Taoism',1,GETDATE())");
            Sql("INSERT INTO ReligionTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (2,N'佛教 Buddhism',1,GETDATE())");
            Sql("INSERT INTO ReligionTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (3,N'基督教 Christian',1,GETDATE())");
            Sql("INSERT INTO ReligionTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (4,N'印度教 Hindu',1,GETDATE())");
            Sql("INSERT INTO ReligionTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (5,N'伊斯兰教 Islam',1,GETDATE())");
            Sql("INSERT INTO ReligionTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (6,N'其它 Other',1,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
