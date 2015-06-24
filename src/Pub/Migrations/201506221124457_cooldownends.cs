namespace Pub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cooldownends : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CooldownEnds", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CooldownEnds");
        }
    }
}
