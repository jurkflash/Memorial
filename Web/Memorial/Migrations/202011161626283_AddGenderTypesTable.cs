namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenderTypesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GenderTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GenderTypes");
        }
    }
}
