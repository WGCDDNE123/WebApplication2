﻿namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBirthDateFromCustomer : DbMigration
    {
        public override void Up()
        {
             DropColumn("dbo.Customers", "BirthDate");
        }
        
        public override void Down()
        {
            
        }
    }
}
