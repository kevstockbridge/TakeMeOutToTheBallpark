namespace Ballpark.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoVenueUserIDMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Venue", "OwnerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Venue", "OwnerID", c => c.Guid(nullable: false));
        }
    }
}
