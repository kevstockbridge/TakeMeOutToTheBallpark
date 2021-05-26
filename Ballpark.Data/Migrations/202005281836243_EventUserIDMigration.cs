namespace Ballpark.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventUserIDMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "OwnerID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "OwnerID");
        }
    }
}
