namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateColorCodeToSpacesTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Spaces SET ColorCode='BCC614' WHERE id=1");
            Sql("UPDATE Spaces SET ColorCode='91876E' WHERE id=2");
            Sql("UPDATE Spaces SET ColorCode='9F8423' WHERE id=3");
            Sql("UPDATE Spaces SET ColorCode='0A5CA8' WHERE id=4");
            Sql("UPDATE Spaces SET ColorCode='21FE08' WHERE id=5");
            Sql("UPDATE Spaces SET ColorCode='9CD9A0' WHERE id=6");
            Sql("UPDATE Spaces SET ColorCode='3E6668' WHERE id=7");
            Sql("UPDATE Spaces SET ColorCode='8E0A50' WHERE id=8");
            Sql("UPDATE Spaces SET ColorCode='7F308F' WHERE id=9");
            Sql("UPDATE Spaces SET ColorCode='B35788' WHERE id=10");
            Sql("UPDATE Spaces SET ColorCode='4886A8' WHERE id=11");
            Sql("UPDATE Spaces SET ColorCode='D13CCC' WHERE id=12");
            Sql("UPDATE Spaces SET ColorCode='3F7C96' WHERE id=13");
            Sql("UPDATE Spaces SET ColorCode='F9A249' WHERE id=14");
            Sql("UPDATE Spaces SET ColorCode='DAE486' WHERE id=15");
            Sql("UPDATE Spaces SET ColorCode='3BE305' WHERE id=16");
            Sql("UPDATE Spaces SET ColorCode='FE63E8' WHERE id=17");
            Sql("UPDATE Spaces SET ColorCode='D45399' WHERE id=18");
            Sql("UPDATE Spaces SET ColorCode='33864E' WHERE id=19");
            Sql("UPDATE Spaces SET ColorCode='BBF54D' WHERE id=20");
        }
        
        public override void Down()
        {
        }
    }
}
