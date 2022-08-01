namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicantsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IC = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 255),
                        Name2 = c.String(),
                        Address = c.String(),
                        HousePhone = c.String(),
                        MobileNumber = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Applicants");
        }
    }
}
