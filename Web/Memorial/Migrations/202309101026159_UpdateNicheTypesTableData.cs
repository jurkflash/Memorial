namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNicheTypesTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE NicheTypes SET Name=N'单 Single' WHERE Id = 1");
            Sql("UPDATE NicheTypes SET Name=N'双 Double' WHERE Id = 2");
        }

        public override void Down()
        {
        }
    }
}
