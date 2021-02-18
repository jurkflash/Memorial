namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaritalTypesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaritalTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Marital = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MaritalTypes");
        }
    }
}
