namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNationalityTypesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NationalityTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Nationality = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NationalityTypes");
        }
    }
}
