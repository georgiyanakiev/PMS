namespace PMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccommodationPackages", "AccommodationPackageID", c => c.Int(nullable: false));
            AddColumn("dbo.AccommodationPackages", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccommodationPackages", "Description");
            DropColumn("dbo.AccommodationPackages", "AccommodationPackageID");
        }
    }
}
