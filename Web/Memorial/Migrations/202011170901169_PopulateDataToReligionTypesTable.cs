namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToReligionTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ReligionTypes(Id,Name) VALUES (1,N'道教 Taoism')");
            Sql("INSERT INTO ReligionTypes(Id,Name) VALUES (2,N'佛教 Buddhism')");
            Sql("INSERT INTO ReligionTypes(Id,Name) VALUES (3,N'基督教 Christian')");
            Sql("INSERT INTO ReligionTypes(Id,Name) VALUES (4,N'印度教 Hindu')");
            Sql("INSERT INTO ReligionTypes(Id,Name) VALUES (5,N'伊斯兰教 Islam')");
            Sql("INSERT INTO ReligionTypes(Id,Name) VALUES (6,N'其它 Other')");
        }
        
        public override void Down()
        {
        }
    }
}
