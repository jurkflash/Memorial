﻿namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFuneralCompaniesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FuneralCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        ContactPerson = c.String(maxLength: 255),
                        ContactNumber = c.String(maxLength: 255),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FuneralCompanies");
        }
    }
}
