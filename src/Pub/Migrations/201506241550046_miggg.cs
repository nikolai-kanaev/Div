namespace Pub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miggg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfileImageUri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfileImageUri");
        }
    }
}
